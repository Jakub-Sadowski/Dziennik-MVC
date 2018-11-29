﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.DAL;
using Dziennik.Models;
using Dziennik.ActionAttrs;


namespace Dziennik.Controllers
{
    [RedirectIfNotAdmin]
    public class KlasaController : Controller
    {
        private Context db = new Context();

        // GET: Klasa
        public ActionResult Index()
        {
            var klasy = db.Klasy.Include(k => k.Wychowawca);
            return View(klasy.ToList());
        }

        // GET: Klasa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa klasa = db.Klasy.Find(id);
            if (klasa == null)
            {
                return HttpNotFound();
            }
            return View(klasa);
        }

        // GET: Klasa/Create
        public ActionResult Create()
        {
            ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            return View();
        }

        // POST: Klasa/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KlasaID,nazwa,level,WychowawcaID")] Klasa klasa)
        {


            List<Klasa> klaska = db.Klasy.Where(a => a.nazwa == klasa.nazwa).ToList();
            if (klaska.Count != 0)
            {
                ModelState.AddModelError("", "Podana klasa istnieje w bazie.");
            }
            else
            if (ModelState.IsValid)
            {
                db.Klasy.Add(klasa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", klasa.WychowawcaID);
            return View(klasa);
        }

        // GET: Klasa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa klasa = db.Klasy.Find(id);
            if (klasa == null)
            {
                return HttpNotFound();
            }
            ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", klasa.WychowawcaID);
            return View(klasa);
        }

        // POST: Klasa/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlasaID,nazwa,level,WychowawcaID")] Klasa klasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", klasa.WychowawcaID);
            return View(klasa);
        }

        // GET: Klasa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa klasa = db.Klasy.Find(id);
            if (klasa == null)
            {
                return HttpNotFound();
            }
            return View(klasa);
        }

        // POST: Klasa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klasa klasa = db.Klasy.Find(id);
            db.Klasy.Remove(klasa);
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
