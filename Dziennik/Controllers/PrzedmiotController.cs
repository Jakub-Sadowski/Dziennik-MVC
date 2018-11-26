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
    public class PrzedmiotController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var przedmioty = db.Przedmioty.Include(p => p.Tresc_ksztalcenia);
            return View(przedmioty.ToList());
        }

        public ActionResult Details(int? id)
        {
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

        public ActionResult Create()
        {
            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nazwa,level")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                db.Przedmioty.Add(przedmiot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmioty.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nazwa,level")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przedmiot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
            return View(przedmiot);
        }

        public ActionResult Delete(int? id)
        {
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
            Przedmiot przedmiot = db.Przedmioty.Find(id);
            db.Przedmioty.Remove(przedmiot);
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
