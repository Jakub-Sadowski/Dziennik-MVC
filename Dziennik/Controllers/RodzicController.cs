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
    public class RodzicController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

            var rodzice = from s in db.Rodzice
                            select s;
            if (!String.IsNullOrEmpty(search))
            {
                rodzice = rodzice.Where(s => s.nazwisko.Contains(search)
                                       || s.imie.Contains(search));
            }
            rodzice = rodzice.OrderByDescending(s => s.nazwisko);
            return View(rodzice.ToList());
        }

        public ActionResult Details(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        public ActionResult Create()
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Rodzice.Add(rodzic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(rodzic);
        }

        public ActionResult Edit(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Rodzic rodzic)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.login == rodzic.login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            if (ModelState.IsValid)
            {
                db.Entry(rodzic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(rodzic);
        }

        public ActionResult Delete(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzic rodzic = db.Rodzice.Find(id);
            if (rodzic == null)
            {
                return HttpNotFound();
            }
            return View(rodzic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Rodzic rodzic = db.Rodzice.Find(id);
            db.Rodzice.Remove(rodzic);
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
