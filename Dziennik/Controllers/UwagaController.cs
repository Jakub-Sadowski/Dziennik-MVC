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
    public class UwagaController : Controller
    {
        private Context db = new Context();

        // GET: Uwaga
        public ActionResult Index()
        {
            if (Session["Status"] != "Admin"|| (Session["Status"] != "Nauczyciel"))
                return RedirectToAction("Index", "Home");
            var uwagi = db.Uwagi.Include(u => u.Nauczyciel).Include(u => u.Uczen);
            return View(uwagi.ToList());
        }

        // GET: Uwaga/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uwaga uwaga = db.Uwagi.Find(id);
            if (uwaga == null)
            {
                return HttpNotFound();
            }
            return View(uwaga);
        }

        // GET: Uwaga/Create
        public ActionResult Create()
        {
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "imie");
            return View();
        }

        // POST: Uwaga/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,UczenID,naglowek,tresc,date")] Uwaga uwaga)
        {
            if (ModelState.IsValid)
            {
                db.Uwagi.Add(uwaga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", uwaga.NauczycielID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "imie", uwaga.UczenID);
            return View(uwaga);
        }

        // GET: Uwaga/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uwaga uwaga = db.Uwagi.Find(id);
            if (uwaga == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", uwaga.NauczycielID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "imie", uwaga.UczenID);
            return View(uwaga);
        }

        // POST: Uwaga/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,UczenID,naglowek,tresc,date")] Uwaga uwaga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uwaga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", uwaga.NauczycielID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "imie", uwaga.UczenID);
            return View(uwaga);
        }

        // GET: Uwaga/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uwaga uwaga = db.Uwagi.Find(id);
            if (uwaga == null)
            {
                return HttpNotFound();
            }
            return View(uwaga);
        }

        // POST: Uwaga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uwaga uwaga = db.Uwagi.Find(id);
            db.Uwagi.Remove(uwaga);
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
