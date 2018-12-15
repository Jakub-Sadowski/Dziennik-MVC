using Dziennik.ActionAttrs;
using Dziennik.DAL;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    [RedirectIfNotAdmin]
    public class UczenController : Controller
    {
        private Context db = new Context();

        // GET: Uczen
        public ActionResult Index(string search)
        {
            var uczniowie = from s in db.Uczniowie
                         select s;
            if (!String.IsNullOrEmpty(search))
            {
                uczniowie = uczniowie.Where(s => (s.imie + " " + s.nazwisko).Contains(search));
            }
            uczniowie = uczniowie.OrderByDescending(s => s.nazwisko);
            return View(uczniowie.ToList());
        }

        // GET: Uczen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic obj = new ExpandoObject();
            obj.uczen = db.Uczniowie.Find(id);
            obj.oceny = db.Oceny.Include(s => s.Nauczyciel).Where(s => s.UczenID == id);
            obj.spoznienia = db.Spoznienia.Include(s => s.Lekcja).Where(s => s.UczenID == id);
            obj.nieobecnosci = db.Nieobecnosci.Include(s => s.Lekcja).Where(s => s.UczenID == id);
            obj.testy = db.Testy_ucznia.Where(s => s.UczenID == id);
            

            return View(obj);
        }

        // GET: Uczen/Create
        public ActionResult Create()
        {
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName");
            return View();
        }

        // POST: Uczen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
        {
            List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.login == uczen.login).ToList();
            if (uczeniowie.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }

            if (ModelState.IsValid)
            {
                db.Uczniowie.Add(uczen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        // GET: Uczen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        // POST: Uczen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
        {
            List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.login == uczen.login).ToList();
            if (uczeniowie.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(uczen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        // GET: Uczen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            return View(uczen);
        }

        // POST: Uczen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uczen uczen = db.Uczniowie.Find(id);
            db.Uczniowie.Remove(uczen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Oceny(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var oceny = from s in db.Oceny
                          select s;
            oceny = oceny.Where(s => s.UczenID==id);
                                      
            if (oceny == null)
            {
                return HttpNotFound();
            }
            return View(oceny);
        }
        [HttpPost, ActionName("Oceny")]
        [ValidateAntiForgeryToken]
        public ActionResult Oceny(int id)
        {
            var oceny = from s in db.Oceny
                        select s;
            oceny = oceny.Where(s => s.UczenID==id);
            oceny = oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot);
            //var oceny = db.Oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot).Include(o => o.Uczen).Where(s => s.UczenID == id);
            return View(oceny.ToList());
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
