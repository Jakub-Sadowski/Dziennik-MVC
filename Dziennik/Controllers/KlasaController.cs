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
using Dziennik.ActionAttrs;


namespace Dziennik.Controllers
{
    public class KlasaController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var klasy = db.Klasy.Include(k => k.Wychowawca);
            return View(klasy.ToList());
        }

        public ActionResult Details(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Create()
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KlasaID,nazwa,level,WychowawcaID")] Klasa klasa)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Edit(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlasaID,nazwa,level,WychowawcaID")] Klasa klasa)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                db.Entry(klasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WychowawcaID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", klasa.WychowawcaID);
            return View(klasa);
        }

        public ActionResult Delete(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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
