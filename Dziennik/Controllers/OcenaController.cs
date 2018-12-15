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
    public class OcenaController : Controller
    {
        private Context db = new Context();

        // GET: Ocena
        public ActionResult Index()
        {
            var oceny = db.Oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot).Include(o => o.Uczen);
            return View(oceny.ToList());
        }

        // GET: Ocena/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            return View(ocena);
        }

        // GET: Ocena/Create
        public ActionResult Create()
        {
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa");
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "imie");
            return View();
        }

        // POST: Ocena/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ocena,waga,data,tresc,PrzedmiotID,NauczycielID,UczenID")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                db.Oceny.Add(ocena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // GET: Ocena/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // POST: Ocena/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ocena,waga,data,tresc,PrzedmiotID,NauczycielID,UczenID")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocena).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // GET: Ocena/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            return View(ocena);
        }

        // POST: Ocena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocena ocena = db.Oceny.Find(id);
            db.Oceny.Remove(ocena);
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
