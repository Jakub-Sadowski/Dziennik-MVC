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
    public class DonosController : Controller
    {
        private Context db = new Context();

        // GET: Donos
        public ActionResult Index()
        {
            var donos = db.Donos.Include(d => d.Nauczyciel).Include(d => d.Uczen);
            return View(donos.ToList());
        }

        // GET: Donos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // GET: Donos/Create
        public ActionResult Create()
        {
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            //ViewBag.RodzicID = new SelectList(db.Rodzics, "ID", "imie");
            return View();
        }

        // POST: Donos/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,RodzicID,pytanie,odpowiedz,data_pytania,data_odpowiedz")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Donos.Add(donos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", donos.NauczycielID);
            var user = Session["UserID"];
            string ide = user.ToString();
            int id = Convert.ToInt32(ide);
            donos.UczenID = id;
            donos.Uczen = db.Uczniowie.Find(id);
            return View(donos);
        }

        // GET: Donos/Edit/5
      /*  public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", donos.NauczycielID);
            return View(donos);
        }*/

        // POST: Donos/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,RodzicID,pytanie,odpowiedz,data_pytania,data_odpowiedz")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciels, "NauczycielID", "imie", donos.NauczycielID);
            ViewBag.RodzicID = new SelectList(db.Rodzics, "ID", "imie", donos.RodzicID);
            return View(donos);
        }
        */
        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donos donos = db.Donos.Find(id);
            db.Donos.Remove(donos);
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
