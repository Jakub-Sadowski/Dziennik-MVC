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
    public class NauczycielController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            return View(db.Nauczyciele.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Nauczyciele.Add(nauczyciel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nauczyciel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nauczyciel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nauczyciel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            db.Nauczyciele.Remove(nauczyciel);
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
