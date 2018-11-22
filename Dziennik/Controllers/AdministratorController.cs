using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.DAL;
using Dziennik.Models;

namespace Dziennik.Controllers
{
    public class AdministratorController : Controller
    {
        private Context db = new Context();

        // GET: Administrator
        public ActionResult Index()
        {
            return View(db.Administratorzy.ToList());
        }
        
        // GET: Administrator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administratorzy.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administratorzy.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administrator);
        }

        // GET: Administrator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administratorzy.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        // GET: Administrator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administratorzy.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administratorzy.Find(id);
            db.Administratorzy.Remove(administrator);
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
