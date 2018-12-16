using Dziennik.ActionAttrs;
using Dziennik.DAL;
using Dziennik.Helpers;
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
    public class UczenController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var uczniowie = from s in db.Uczniowie
                         select s;
            if (!String.IsNullOrEmpty(search))
            {
                uczniowie = uczniowie.Where(s => (s.imie + " " + s.nazwisko).Contains(search));
            }
            uczniowie = uczniowie.OrderByDescending(s => s.nazwisko);
            return View(uczniowie.ToList());
        }

        public ActionResult Details(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Create()
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Edit(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Delete(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Uczen uczen = db.Uczniowie.Find(id);
            db.Uczniowie.Remove(uczen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Oceny(int? id)
        {
            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                 id = Convert.ToInt32(ide);
            }
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
            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);
                var oceny = from s in db.Oceny
                            select s;
                oceny = oceny.Where(s => s.UczenID == id1);
                oceny = oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot);
                return View(oceny.ToList());
            }
            else
            {
                var oceny = from s in db.Oceny
                            select s;
                oceny = oceny.Where(s => s.UczenID == id);
                oceny = oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot);
                return View(oceny.ToList());
            }
        }

		public ActionResult Przedmioty()
		{
			if (Session["Status"] != "Uczeń")
				return RedirectToAction("Index", "Home");

			var userId = Convert.ToInt32(Session["UserID"]);
			var klasa = db.Klasy
				.Include(k => k.Uczniowie)
				.Where(k => k.Uczniowie.Any(u => u.ID == userId))
				.SingleOrDefault();

			var przedmioty = db.Lekcja
				.Where(l => l.KlasaID == klasa.KlasaID)
				.Include(l => l.Przedmiot)
				.Include("Przedmiot.Tresc_ksztalcenia")
				.Select(l => l.Przedmiot).ToList();
			foreach(var p in przedmioty)
			{
				p.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(p.Tresc_ksztalcenia.plikSciezka);
			}

			return View(przedmioty);
		}

		public ActionResult SzczegolyPrzedmiotu(int? id)
		{
			if (Session["Status"] != "Uczeń")
				return RedirectToAction("Index", "Home");

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var userId = Convert.ToInt32(Session["UserID"]);
			var klasa = db.Klasy
				.Include(k => k.Uczniowie)
				.Where(k => k.Uczniowie.Any(u => u.ID == userId))
				.SingleOrDefault();

			var przedmiot = db.Przedmioty
				.Where(p => p.ID == id)
				.Include(p => p.Testy)
				.Include(p => p.Pliki)
				.Where(p => p.Testy.Where(
					t => t.KlasaID == klasa.KlasaID)
					.Any())
				.SingleOrDefault()
				;

			return View(przedmiot);
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
