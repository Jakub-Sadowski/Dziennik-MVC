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
    public class Ogloszenie_dla_rodzicowController : Controller
    {
        private Context db = new Context();

        // GET: Ogloszenie_dla_rodzicow
        public ActionResult Index()
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var ogloszenia_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Include(o => o.klasa).Include(o => o.Nauczyciel);
            return View(ogloszenia_dla_rodzicow.ToList());
        }

        // GET: Ogloszenie_dla_rodzicow/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Create
        public ActionResult Create()
        {
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            return View();
        }

        // POST: Ogloszenie_dla_rodzicow/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,KlasaID,naglowek,tresc,data")] Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                ogloszenie_dla_rodzicow.data = DateTime.Now;
                db.Ogloszenia_dla_rodzicow.Add(ogloszenie_dla_rodzicow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie_dla_rodzicow.NauczycielID);
            
            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie_dla_rodzicow.NauczycielID);
            return View(ogloszenie_dla_rodzicow);
        }

        // POST: Ogloszenie_dla_rodzicow/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,KlasaID,naglowek,tresc,data")] Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                ogloszenie_dla_rodzicow.data = DateTime.Now;
                db.Entry(ogloszenie_dla_rodzicow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie_dla_rodzicow.NauczycielID);
           
            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie_dla_rodzicow);
        }

        // POST: Ogloszenie_dla_rodzicow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            db.Ogloszenia_dla_rodzicow.Remove(ogloszenie_dla_rodzicow);
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
