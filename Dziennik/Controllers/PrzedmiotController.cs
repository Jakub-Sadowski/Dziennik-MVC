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
using Dziennik.Helpers;
using Dziennik.Models;

namespace Dziennik.Controllers
{
    public class PrzedmiotController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string nazwa)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			string databaseName = db.Database.Connection.Database;
            var przedmioty = db.Przedmioty.Include(p => p.Tresc_ksztalcenia);
            if (!String.IsNullOrEmpty(nazwa))
            {
                przedmioty = przedmioty.Where(p => p.nazwa == nazwa);
            }
			foreach(var p in przedmioty)
			{
				p.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(p.Tresc_ksztalcenia.plikSciezka);
			}
            return View(przedmioty.ToList());
        }

        public ActionResult Details(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmioty
                .Include(p => p.Lekcje)
                .Include("Lekcje.Nauczyciel")
                .Include("Lekcje.Klasa")
                .Include(p => p.Oceny)
                .Include("Oceny.Nauczyciel")
                .Include("Oceny.Uczen")
                .Include(p => p.Pliki)
                .Include(p => p.Testy)
                .Include("Testy.Nauczyciel")
                .Include("Testy.Klasa")
                .Include(p => p.Tresc_ksztalcenia)
                .Where(p => p.ID == id).FirstOrDefault();
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            przedmiot.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(przedmiot.Tresc_ksztalcenia.plikSciezka);
            return View(przedmiot);
        }

        public ActionResult Create()
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nazwa,level")] Przedmiot przedmiot, HttpPostedFileBase fileUpload)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                var sciezka = FileHandler.saveFile(fileUpload);
                var tk = new Tresc_ksztalcenia(przedmiot.ID, sciezka);
                przedmiot.Tresc_ksztalcenia = tk;
                db.Przedmioty.Add(przedmiot);
                db.Tresci_ksztalcenia.Add(tk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        public ActionResult Edit(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmioty.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            przedmiot.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(przedmiot.Tresc_ksztalcenia.plikSciezka);
            return View(przedmiot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nazwa,level")] Przedmiot przedmiot, HttpPostedFileBase fileUpload)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                Tresc_ksztalcenia oldTk = null;
                string sciezka = null;
                if (fileUpload != null)
                {
                    oldTk = db.Tresci_ksztalcenia.Find(przedmiot.ID);
                    sciezka = FileHandler.saveFile(fileUpload);
                    var tk = new Tresc_ksztalcenia(przedmiot.ID, sciezka);
                    przedmiot.Tresc_ksztalcenia = tk;
                    db.Tresci_ksztalcenia.Remove(oldTk);
                    db.Tresci_ksztalcenia.Add(tk);
                }

                db.Entry(przedmiot).State = EntityState.Modified;
                db.SaveChanges();

                if (oldTk != null)
                    FileHandler.deleteFile(oldTk.plikSciezka);

                return RedirectToAction("Index");
            }
            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        public ActionResult Delete(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmioty.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            return View(przedmiot);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Przedmiot przedmiot = db.Przedmioty.Find(id);
            var tk = db.Tresci_ksztalcenia.Find(przedmiot.ID);
            db.Przedmioty.Remove(przedmiot);
            db.Tresci_ksztalcenia.Remove(tk);
            db.SaveChanges();

            FileHandler.deleteFile(tk.plikSciezka);
            return RedirectToAction("Index");
        }

        public ActionResult DownloadTrescKsztalcenia(int? id)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var tk = db.Tresci_ksztalcenia.Find(id);
            if (tk == null)
                throw new ArgumentException();
            var path = tk.plikSciezka;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = FileHandler.getFileName(path);
            var mime = MimeMapping.GetMimeMapping(fileName);
            return File(fileBytes, mime, fileName);
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
