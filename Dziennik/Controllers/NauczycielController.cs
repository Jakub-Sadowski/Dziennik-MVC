using Dziennik.DAL;
using Dziennik.Helpers;
using Dziennik.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class NauczycielController : Controller
    {
        private Context db = new Context();

		#region CRUD
		public ActionResult Index()
        {
            return View(db.Nauczyciele.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Nauczyciele.Add(nauczyciel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nauczyciel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nauczyciel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nauczyciel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            if (nauczyciel == null)
            {
                return HttpNotFound();
            }
            return View(nauczyciel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var list = db.Klasy.Where(s => s.WychowawcaID == id);
            foreach (var a in list)
            {
                a.WychowawcaID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list1 = db.Lekcja.Where(s => s.NauczycielID == id);
            foreach (var a in list1)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list2 = db.Pliki.Where(s => s.NauczycielID == id);
            foreach (var a in list2)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list3 = db.Testy.Where(s => s.NauczycielID == id);
            foreach (var a in list3)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list4 = db.Uwagi.Where(s => s.NauczycielID == id);
            foreach (var a in list4)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list5 = db.Ogloszenia.Where(s => s.NauczycielID == id);
            foreach (var a in list5)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list6 = db.Ogloszenia_dla_rodzicow.Where(s => s.NauczycielID == id);
            foreach (var a in list6)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();

            var list7 = db.Oceny.Where(s => s.NauczycielID == id);
            foreach (var a in list7)
            {
                a.NauczycielID = null;
                db.Entry(a).State = EntityState.Modified;



            }
            db.SaveChanges();





            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            db.Nauczyciele.Remove(nauczyciel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
		#endregion

		#region Przedmioty
		public ActionResult Przedmioty()
		{
			if (Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");

			var userId = Convert.ToInt32(Session["UserID"]);

			var przedmioty = db.Lekcja
				.Where(l => l.NauczycielID == userId)
				.Include(l => l.Przedmiot)
				.Include("Przedmiot.Tresc_ksztalcenia")
				.Select(l => l.Przedmiot).ToList();
			foreach (var p in przedmioty)
			{
				p.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(p.Tresc_ksztalcenia.plikSciezka);
			}

			return View(przedmioty);
		}

		public ActionResult PlikiPrzedmiotu(int? id)
		{
			if (Session["Status"] != "Nauczyciel")
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
			foreach (var file in przedmiot.Pliki)
			{
				file.FilePath = FileHandler.getFileName(file.FilePath);
			}
			return View(przedmiot);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PlikiPrzedmiotu([Bind(Include = "ID")] Przedmiot przedmiot, HttpPostedFileBase[] fileUpload)
		{
			if (Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");

			if (przedmiot.ID != null)
			{
				string sciezka = null;
				Przedmiot original = db.Przedmioty
					.Include(p => p.Pliki)
					.Include(p => p.Tresc_ksztalcenia)
					.SingleOrDefault(p => p.ID == przedmiot.ID);

				if (fileUpload != null)
				{
					foreach (var file in fileUpload)
					{
						sciezka = FileHandler.saveFile(file);
						var newFile = new Plik { PrzedmiotID = przedmiot.ID, FilePath = sciezka, NauczycielID = Convert.ToInt32(Session["UserID"]), DataDodania = DateTime.Now };
						db.Pliki.Add(newFile);
						original.Pliki.Add(newFile);
					}
				db.SaveChanges();
				}

				return RedirectToAction("Przedmioty");
			}
			ViewBag.Tresc_ksztalcenia = new SelectList(db.Tresci_ksztalcenia, "PrzedmiotID", "PrzedmiotID", przedmiot.ID);
			return View(przedmiot);
		}

		public ActionResult UsunPlik(int? id)
		{
			if (Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");

			Plik plik= db.Pliki.Find(id);
			if(plik == null)
				return RedirectToAction("Error", "Home");
			var przedmiotId = plik.PrzedmiotID;
			db.Pliki.Remove(plik);
			db.SaveChanges();

			FileHandler.deleteFile(plik.FilePath);

			return RedirectToAction("PlikiPrzedmiotu", new { id = przedmiotId });
		}
        #endregion

        public ActionResult PlanNauczyciela(int? id)
        {
            if (Session["Status"] == "Nauczyciel")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                id = Convert.ToInt32(ide);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lekcje = from s in db.Lekcja
                        select s;
            lekcje = lekcje.Where(s => s.NauczycielID == id);

            if (lekcje == null)
            {
                return HttpNotFound();
            }

            return View(lekcje);
        }

        public ActionResult RozkladNauczycieli(string search)
        {
            //var nauczyciele = from s in db.Nauczyciele
            //                  .Include(s=> s.Lekcje)
            //                select s;
            //if (!String.IsNullOrEmpty(search))
            //{
            //    nauczyciele = nauczyciele.Where(s => (s.imie + " " + s.nazwisko).Contains(search));
            //}
            //nauczyciele = nauczyciele.OrderByDescending(s => s.nazwisko);


            //return View(nauczyciele.ToList());
            
            var lekcje = from s in db.Lekcja
                         select s;
            if (!String.IsNullOrEmpty(search))
            {
                lekcje = lekcje.Where(s => (s.Nauczyciel.imie + " " + s.Nauczyciel.nazwisko).Contains(search));
            }

            if (lekcje == null)
            {
                return HttpNotFound();
            }

            return View(lekcje);
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
