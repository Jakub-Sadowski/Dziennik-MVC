using Dziennik.DAL;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class RodzicController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

            var rodzice = from s in db.Rodzice
                            select s;
            if (!String.IsNullOrEmpty(search))
            {
                rodzice = rodzice.Where(s => s.Nazwisko.Contains(search)
                                       || s.Imie.Contains(search));
            }
            rodzice = rodzice.OrderByDescending(s => s.Nazwisko);
            return View(rodzice.ToList());
        }

        public ActionResult Details(int? id)
        {
			if ((string)Session["Status"] != "Admin")
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
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Login,Haslo")] Rodzic rodzic)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.Login == rodzic.Login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany Login istnieje w bazie.");
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
			if ((string)Session["Status"] != "Admin")
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
        public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko,Login,Haslo")] Rodzic rodzic)
        {
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Rodzic> rodzice = db.Rodzice.Where(a => a.Login == rodzic.Login).ToList();
            if (rodzice.Count != 0)
            {
                ModelState.AddModelError("", "Podany Login istnieje w bazie.");
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
			if ((string)Session["Status"] != "Admin")
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
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Rodzic rodzic = db.Rodzice.Find(id);
            db.Rodzice.Remove(rodzic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Ogloszenia()
        {
            if ((string)Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");
            ViewBag.NauczycielID = Session["UserID"];
            int id = Int32.Parse(ViewBag.NauczycielID);
            Rodzic rodzic = db.Rodzice.Find(id);

            var dzieci = from s in db.Uczniowie
                         where s.RodzicID == id
                        select s.KlasaID;
            var ogloszenia = from s in db.Ogloszenia_dla_rodzicow
                             from b in dzieci
                             where s.KlasaID==b
                        select s;
            //var ogloszenia_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Include(o => o.klasa).Include(o => o.Nauczyciel);
            //ogloszenia_dla_rodzicow = ogloszenia_dla_rodzicow.Where(o=>o.KlasaID==dzieci.);
            return View(ogloszenia.ToList());
        }

        public ActionResult Oceny(string data)
        {
            int? id = null;
            if(data != null)
                id = Int32.Parse(data);
                

            if ((string)Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");
        

            int id_rodzic = Int32.Parse((string)Session["UserID"]);
            //Rodzic rodzic = db.Rodzice.Find(id_rodzic);
            var dzieci = db.Uczniowie.Where(s => s.RodzicID == id_rodzic).ToList();
            ViewBag.dzieci = dzieci;

            if (id == null)
            {

                if (dzieci.Count() == 0)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Imie = dzieci[0].Imie;
                    ViewBag.Nazwisko = dzieci[0].Nazwisko;
                    int id_dziecka = dzieci[0].ID;
                    var oceny = db.Oceny.Where(s => s.UczenID == id_dziecka).ToList();
                    return View(oceny);
                }

            }
            else
            {
                Uczen uczen = db.Uczniowie.Find(id);
                if(uczen == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Imie = uczen.Imie;
                ViewBag.Nazwisko = uczen.Nazwisko;
                var oceny = db.Oceny.Where(s => s.UczenID == id).ToList();
                return View(oceny);
            }
        }
        public ActionResult Absencja(string data)
        {
            int? id = null;
            if (data != null)
                id = Int32.Parse(data);

            if ((string)Session["Status"] != "Rodzic")
            {
                return RedirectToAction("Index", "Home");
            }
      

            var id_rodzica = Int32.Parse((string)Session["UserID"]);
            //Rodzic rodzic = db.Rodzice.Find(id);
            var dzieci = db.Uczniowie.Where(s => s.RodzicID == id_rodzica).ToList();
            ViewBag.dzieci = dzieci;
            if (id == null)
            {
                if (dzieci.Count() == 0)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Imie = dzieci[0].Imie;
                    ViewBag.Nazwisko = dzieci[0].Nazwisko;
                    int id_dziecka = dzieci[0].ID;
                    var model = new Absencja();

                    model.Nieobecnosci = GetNieobecnosciModel(id_dziecka);
                    model.Spoznienia = GetSpoznieniaModel(id_dziecka);

                    return View(model);
                }
            }
            else
            {
                Uczen uczen = db.Uczniowie.Find(id);
                if (uczen == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Imie = uczen.Imie;
                ViewBag.Nazwisko = uczen.Nazwisko;
                var model = new Absencja();
                model.Nieobecnosci = GetNieobecnosciModel(uczen.ID);
                model.Spoznienia = GetSpoznieniaModel(uczen.ID);

                return View(model);
            }
        }
        private IEnumerable<Nieobecnosc> GetNieobecnosciModel(int id)
        {
            var nieobecn = from s in db.Nieobecnosci
                           select s;
            nieobecn = nieobecn.Where(s => s.UczenID == id).Include(x => x.Lekcja.Przedmiot);
            return nieobecn.AsEnumerable();
        }

        private IEnumerable<Spoznienie> GetSpoznieniaModel(int id)
        {
            var spoznienia = from s in db.Spoznienia
                             select s;
            spoznienia = spoznienia.Where(s => s.UczenID == id).Include(x => x.Lekcja.Przedmiot);
            return spoznienia.AsEnumerable();
        }
        public ActionResult PlanLekcji(string data)
        {

            int? id = null;
            if (data != null)
                id = Int32.Parse(data);

            if ((string)Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");

            var id_rodzica = Convert.ToInt32(Session["UserID"]);
           
            Rodzic rodzic = db.Rodzice.Find(id_rodzica);
            var dzieci = db.Uczniowie.Where(s => s.RodzicID == id_rodzica).ToList();
            ViewBag.dzieci = dzieci;
            var klasa = db.Klasy
                .Include(k => k.Uczniowie)
                .Where(k => k.Uczniowie.Any(u => u.RodzicID == id_rodzica))
                .SingleOrDefault();



            if (id == null)
            {
                if (dzieci.Count() == 0)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Imie = dzieci[0].Imie;
                    ViewBag.Nazwisko = dzieci[0].Nazwisko;
                    int id_dziecka = dzieci[0].ID;
                    var lekcje = db.Lekcja
                     .Where(l => l.KlasaID == klasa.KlasaID)
                     .ToList();
                    return View(lekcje);
                }
            }
            else
            {
                Uczen uczen = db.Uczniowie.Find(id);
                if (uczen == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Imie = uczen.Imie;
                ViewBag.Nazwisko = uczen.Nazwisko;
                var lekcje = db.Lekcja
                    .Where(l => l.KlasaID == klasa.KlasaID)
                    .ToList();
                return View(lekcje);
            }
        }
        /// <summary>
        ///Uwagi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Uwagi(string data)
        {
            int? id = null;
            if (data != null)
                id = Int32.Parse(data);
            if ((string)Session["Status"] != "Rodzic")
            {
                return RedirectToAction("Index", "Home");
            }
            var id_rodzica = Convert.ToInt32(Session["UserID"]);
            Rodzic rodzic = db.Rodzice.Find(id_rodzica);
            var dzieci = db.Uczniowie.Where(s => s.RodzicID == id_rodzica).ToList();
            ViewBag.dzieci = dzieci;
            if (id == null)
            {
                if (dzieci.Count() == 0)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Imie = dzieci[0].Imie;
                    ViewBag.Nazwisko = dzieci[0].Nazwisko;
                    int id_dziecka = dzieci[0].ID;
                    var uwagi_dziecka = from s in db.Uwagi
                                select s;
                    uwagi_dziecka = uwagi_dziecka.Where(s => s.Uczen.RodzicID == id_rodzica);
                    return View(uwagi_dziecka);
                }
            }
            else
            {
                Uczen uczen = db.Uczniowie.Find(id);
                if (uczen == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                ViewBag.Imie = uczen.Imie;
                ViewBag.Nazwisko = uczen.Nazwisko;
                var uwagi = from s in db.Uwagi
                            select s;
                uwagi = uwagi.Where(s => s.Uczen.RodzicID == id_rodzica);
                uwagi = uwagi.Where(s => s.ID == id);
                return View(uwagi);
            }
        }
        public ActionResult UsuwanieNieobecnosci(int? id)
        {
            if ((string)Session["Status"] != "Rodzic")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nieobecnosc = db.Nieobecnosci.Find(id);
            nieobecnosc.Status = Nieobecnosc.status.Usprawiedliwiona;
            db.Entry(nieobecnosc).State = EntityState.Modified;
            db.SaveChanges();
            return View(nieobecnosc);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodawanieZapytania([Bind(Include = "NauczycielID, pytanie")] Zapytanie zapytanie, string data)
        {
            if ((string)Session["Status"] != "Rodzic")
            {
                return RedirectToAction("Index", "Home");

            }
            var userId = Convert.ToInt32(Session["UserID"]);
            zapytanie.data_pytania = DateTime.Now;
            zapytanie.NauczycielID = Convert.ToInt32(data);
            zapytanie.RodzicID = userId;
            if (ModelState.IsValid)
            {
                db.Zapytania.Add(zapytanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zapytanie);
        }

        public ActionResult Profil(int? id, int? liczba)
        {
            ViewBag.control = liczba;
            if ((string)Session["Status"] != "Rodzic" || Int32.Parse((string)Session["UserID"]) != id)
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
        public ActionResult Zmien_Login(Rodzic rodzic)
        {
            int liczba;
            if ((string)Session["Status"] != "Rodzic" || Int32.Parse((string)Session["UserID"]) != rodzic.ID)
                return RedirectToAction("Index", "Home");

            if (rodzic == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (db.Administratorzy.Where(a => a.Login == rodzic.Login).Count()
                + db.Uczniowie.Where(a => a.Login == rodzic.Login).Count()
                + db.Rodzice.Where(a => a.Login == rodzic.Login).Count()
                + db.Nauczyciele.Where(a => a.Login == rodzic.Login).Count()

                > 0 && db.Rodzice.Find(rodzic.ID).Login != rodzic.Login)
                liczba = 1;

            else
            {
                liczba = 0;
                if (ModelState.IsValid)
                {

                    db.Rodzice.AddOrUpdate(rodzic);
                    db.SaveChanges();

                }

            }

            return RedirectToAction("Profil", "Rodzic", new { id = rodzic.ID, liczba = liczba });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Zmien_haslo(Rodzic rodzic)
        {

            if ((string)Session["Status"] != "Rodzic" || Int32.Parse((string)Session["UserID"]) != rodzic.ID)
                return RedirectToAction("Index", "Home");

            if (rodzic == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (ModelState.IsValid)
            {

                db.Rodzice.AddOrUpdate(rodzic);
                db.SaveChanges();

            }



            return RedirectToAction("Profil", "Rodzic", new { id = rodzic.ID });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Zmien_email(Rodzic rodzic)
        {

            if ((string)Session["Status"] != "Rodzic" || Int32.Parse((string)Session["UserID"]) != rodzic.ID)
                return RedirectToAction("Index", "Home");

            if (rodzic == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (ModelState.IsValid)
            {

                db.Rodzice.AddOrUpdate(rodzic);
                db.SaveChanges();

            }



            return RedirectToAction("Profil", "Rodzic", new { id = rodzic.ID });

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
