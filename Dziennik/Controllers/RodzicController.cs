﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.ActionAttrs;
using Dziennik.DAL;
using Dziennik.Models;

namespace Dziennik.Controllers
{
    [RedirectIfNotAdmin]
    public class RodzicController : Controller
    {
        private Context db = new Context();

        // GET: Rodzic
        public ActionResult Index(string search)
        {
            var rodzice = from s in db.Rodzice
                            select s;
            if (!String.IsNullOrEmpty(search))
            {
                rodzice = rodzice.Where(s => s.nazwisko.Contains(search)
                                       || s.imie.Contains(search));
            }
            rodzice = rodzice.OrderByDescending(s => s.nazwisko);
            return View(rodzice.ToList());
        }

        // GET: Rodzic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        // GET: Rodzic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rodzic/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
            List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Rodzice.Add(rodzic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(rodzic);
        }

        // GET: Rodzic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        // POST: Rodzic/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
            List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(rodzic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(rodzic);
        }

        // GET: Rodzic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        // POST: Rodzic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rodzic rodzic = db.Rodzice.Find(id);
            db.Rodzice.Remove(rodzic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
