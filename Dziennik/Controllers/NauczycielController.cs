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
            var list = db.Klasy.Where(s => s.WychowawcaID == id);
            foreach (var a in list)
            {
                a.WychowawcaID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list1 = db.Lekcja.Where(s => s.NauczycielID == id);
            foreach (var a in list1)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list2 = db.Pliki.Where(s => s.NauczycielID == id);
            foreach (var a in list2)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list3 = db.Testy.Where(s => s.NauczycielID == id);
            foreach (var a in list3)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list4 = db.Uwagi.Where(s => s.NauczycielID == id);
            foreach (var a in list4)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list5 = db.Ogloszenia.Where(s => s.NauczycielID == id);
            foreach (var a in list5)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list6 = db.Ogloszenia_dla_rodzicow.Where(s => s.NauczycielID == id);
            foreach (var a in list6)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list7 = db.Oceny.Where(s => s.NauczycielID == id);
            foreach (var a in list7)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();





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
