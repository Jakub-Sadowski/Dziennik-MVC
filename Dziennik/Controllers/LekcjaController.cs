using Dziennik.DAL;
using Dziennik.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class LekcjaController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var lekcje = db.Lekcja.Include(l => l.Klasa).Include(l => l.Nauczyciel).Include(l => l.Przedmiot);
            return View(lekcje.ToList());
        }

        public ActionResult Details(int? id)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            return View(lekcja);
        }

        public ActionResult Create()
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,KlasaID,PrzedmiotID,godzina,dzien")] Lekcja lekcja)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                db.Lekcja.Add(lekcja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        public ActionResult Edit(int? id)
        {
			if ((string)Session["Status"] != "Admin")

				return RedirectToAction("Index", "Home");
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,KlasaID,PrzedmiotID,godzina,dzien")] Lekcja lekcja)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
            {
                db.Entry(lekcja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", lekcja.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", lekcja.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", lekcja.PrzedmiotID);
            return View(lekcja);
        }

        public ActionResult Delete(int? id)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lekcja lekcja = db.Lekcja.Find(id);
            if (lekcja == null)
            {
                return HttpNotFound();
            }
            return View(lekcja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

            Lekcja lekcja = db.Lekcja.Find(id);

            var nieobecnosci = db.Nieobecnosci.Where(s => s.LekcjaID == id);
            foreach(var a in nieobecnosci)
            {
                a.LekcjaID = null;
                db.Entry(a).State = EntityState.Modified;
                
                

            }
            db.SaveChanges();

            var spoznienie = db.Spoznienia.Where(s => s.LekcjaID == id);
            foreach (var a in spoznienie)
            {
                a.LekcjaID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

			//Lekcja lekcja = db.Lekcja.Find(id);
            db.Lekcja.Remove(lekcja);
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
