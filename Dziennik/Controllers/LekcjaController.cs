using System;
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
    public class LekcjaController : Controller
    {
        private Context db = new Context();

        // GET: Lekcja
        public ActionResult Index()
        {
            var lekcje = db.Lekcja.Include(l => l.Klasa).Include(l => l.Nauczyciel).Include(l => l.Przedmiot);
            return View(lekcje.ToList());
        }

        // GET: Lekcja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            return View(lekcja);
        }

        // GET: Lekcja/Create
        public ActionResult Create()
        {
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa");
            return View();
        }

        // POST: Lekcja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,KlasaID,PrzedmiotID,godzina,dzien")] Lekcja lekcja)
        {
            if (ModelState.IsValid)
            {
                db.Lekcja.Add(lekcja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        // GET: Lekcja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        // POST: Lekcja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,KlasaID,PrzedmiotID,godzina,dzien")] Lekcja lekcja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lekcja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        // GET: Lekcja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            return View(lekcja);
        }

        // POST: Lekcja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

           
             var nieobecnosci = db.Nieobecnosci.Where(s => s.LekcjaID == id);
            foreach(var a in nieobecnosci)
            {
                a.LekcjaID = null;
                db.Entry(a).State = EntityState.Modified;
                
                

            }
            db.SaveChanges();

            var spoznienie = db.Spoznienia.Where(s => s.LekcjaID == id);
            foreach (var a in spoznienie)
            {
                a.LekcjaID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();


            Lekcja lekcja = db.Lekcja.Find(id);
            db.Lekcja.Remove(lekcja);
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
