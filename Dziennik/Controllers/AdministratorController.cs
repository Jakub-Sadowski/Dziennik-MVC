using Dziennik.DAL;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class AdministratorController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var admini = from s in db.Administratorzy
                           select s;
            if (!String.IsNullOrEmpty(search))
            {
                admini = admini.Where(s => (s.imie+" "+s.nazwisko).Contains(search));
            }
            admini = admini.OrderByDescending(s => s.nazwisko);
            return View(admini.ToList());
        }
        
        public ActionResult Details(int? id)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        public ActionResult Create()
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo")] Administrator administrator)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Administrator> admin = db.Administratorzy.Where(a => a.login == administrator.login).ToList();
            if(admin.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            else  

            if (ModelState.IsValid)
            {
                db.Administratorzy.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(administrator);
        }

        public ActionResult Edit(int? id)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo")] Administrator administrator)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Administrator> admin = db.Administratorzy.Where(a => a.login == administrator.login).ToList();
            if (admin.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }
            else
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        public ActionResult Delete(int? id)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

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
