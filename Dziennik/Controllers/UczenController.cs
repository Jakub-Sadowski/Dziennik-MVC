using Dziennik.DAL;
using Dziennik.Helpers;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class UczenController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string search)
        {
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var uczniowie = from s in db.Uczniowie
                         select s;
            if (!String.IsNullOrEmpty(search))
            {
                uczniowie = uczniowie.Where(s => (s.imie + " " + s.nazwisko).Contains(search));
            }
            uczniowie = uczniowie.OrderByDescending(s => s.nazwisko);
            return View(uczniowie.ToList());
        }

        public ActionResult Details(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic obj = new ExpandoObject();
            obj.uczen = db.Uczniowie.Find(id);
            obj.oceny = db.Oceny.Include(s => s.Nauczyciel).Where(s => s.UczenID == id);
            obj.spoznienia = db.Spoznienia.Include(s => s.Lekcja).Where(s => s.UczenID == id);
            obj.nieobecnosci = db.Nieobecnosci.Include(s => s.Lekcja).Where(s => s.UczenID == id);
            obj.testy = db.Testy_ucznia.Where(s => s.UczenID == id);
            

            return View(obj);
        }

        public ActionResult Create()
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.login == uczen.login).ToList();
            if (uczeniowie.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }

            if (ModelState.IsValid)
            {
                db.Uczniowie.Add(uczen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        public ActionResult Edit(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,imie,nazwisko,login,haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.login == uczen.login).ToList();
            if (uczeniowie.Count != 0)
            {
                ModelState.AddModelError("", "Podany login istnieje w bazie.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(uczen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", uczen.KlasaID);
            ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName", uczen.RodzicID);
            return View(uczen);
        }

        public ActionResult Delete(int? id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uczen uczen = db.Uczniowie.Find(id);
            if (uczen == null)
            {
                return HttpNotFound();
            }
            return View(uczen);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
		{
			if (Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Uczen uczen = db.Uczniowie.Find(id);
            db.Uczniowie.Remove(uczen);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Oceny(int? id)
        {
            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                 id = Convert.ToInt32(ide);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var oceny = from s in db.Oceny
                          select s;
            oceny = oceny.Where(s => s.UczenID==id);
                                      
            if (oceny == null)
            {
                return HttpNotFound();
            }
            
            return View(oceny);
        }
        [HttpPost, ActionName("Oceny")]
        [ValidateAntiForgeryToken]
        public ActionResult Oceny(int id)
        {
            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);
                var oceny = from s in db.Oceny
                            select s;
                oceny = oceny.Where(s => s.UczenID == id1);
                oceny = oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot);
                return View(oceny.ToList());
            }
            else
            {
                var oceny = from s in db.Oceny
                            select s;
                oceny = oceny.Where(s => s.UczenID == id);
                oceny = oceny.Include(o => o.Nauczyciel).Include(o => o.Przedmiot);
                return View(oceny.ToList());
            }
        }

		public ActionResult Przedmioty()
		{
			if (Session["Status"] != "Uczeń")
				return RedirectToAction("Index", "Home");

			var userId = Convert.ToInt32(Session["UserID"]);
			var klasa = db.Klasy
				.Include(k => k.Uczniowie)
				.Where(k => k.Uczniowie.Any(u => u.ID == userId))
				.SingleOrDefault();

			var przedmioty = db.Lekcja
				.Where(l => l.KlasaID == klasa.KlasaID)
				.Include(l => l.Przedmiot)
				.Include("Przedmiot.Tresc_ksztalcenia")
				.Select(l => l.Przedmiot).ToList();
			foreach(var p in przedmioty)
			{
				p.Tresc_ksztalcenia.plikSciezka = FileHandler.getFileName(p.Tresc_ksztalcenia.plikSciezka);
			}

			return View(przedmioty);
		}

		public ActionResult SzczegolyPrzedmiotu(int? id)
		{
			if (Session["Status"] != "Uczeń")
				return RedirectToAction("Index", "Home");

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var userId = Convert.ToInt32(Session["UserID"]);
			var klasa = db.Klasy
				.Include(k => k.Uczniowie)
				.Where(k => k.Uczniowie.Any(u => u.ID == userId))
				.SingleOrDefault();

			var przedmiot = db.Przedmioty
				.Where(p => p.ID == id)
				.Include(p => p.Testy)
				.Include(p => p.Pliki)
				.Where(p => p.Testy.Where(
					t => t.KlasaID == klasa.KlasaID)
					.Any())
				.SingleOrDefault()
				;

			return View(przedmiot);
		}

        public ActionResult TworzenieOceny()
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName");
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa");
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName");
            return View();
        }

        // POST: Ocena/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TworzenieOceny([Bind(Include = "ID,ocena,waga,data,tresc,PrzedmiotID,NauczycielID,UczenID")] Ocena ocena)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Oceny.Add(ocena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // GET: Ocena/Edit/5
        public ActionResult EdytowanieOceny(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // POST: Ocena/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EdytowanieOceny([Bind(Include = "ID,ocena,waga,data,tresc,PrzedmiotID,NauczycielID,UczenID")] Ocena ocena)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(ocena).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName", ocena.NauczycielID);
            ViewBag.PrzedmiotID = new SelectList(db.Przedmioty, "ID", "nazwa", ocena.PrzedmiotID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", ocena.UczenID);
            return View(ocena);
        }

        // GET: Ocena/Delete/5
        public ActionResult UsuwanieOceny(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Oceny.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            return View(ocena);
        }

        // POST: Ocena/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult UsuwanieOcenyPotwierdzone(int id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            Ocena ocena = db.Oceny.Find(id);
            db.Oceny.Remove(ocena);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Absencja(int? id)
        {
            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                id = Convert.ToInt32(ide);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new Absencja();

            model.Nieobecnosci = GetNieobecnosciModel(id.Value);
            model.Spoznienia = GetSpoznieniaModel(id.Value);

            return View(model);
        }
        [HttpPost, ActionName("Absencja")]
        [ValidateAntiForgeryToken]
        public ActionResult Absencja(int id)
        {
            Absencja model = new Absencja();

            if (Session["Status"] == "Uczeń")
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);                

                model.Nieobecnosci = GetNieobecnosciModel(id1);
                model.Spoznienia = GetSpoznieniaModel(id1);

                return View(model);
            }
            else
            {
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);

                model.Nieobecnosci = GetNieobecnosciModel(id1);
                model.Spoznienia = GetSpoznieniaModel(id1);

                return View(model);
            }
        }

        

        public ActionResult DodawanieNieobecnosci()
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID");
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName");
            return View();
        }

        // POST: Ocena/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodawanieNieobecnosci([Bind(Include = "ID,UczenID,LekcjaID, date")] Nieobecnosc nieobecnosc)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Nieobecnosci.Add(nieobecnosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", nieobecnosc.LekcjaID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", nieobecnosc.UczenID);
            return View(nieobecnosc);
        }

        // GET: Ocena/Edit/5
        public ActionResult EdytowanieNieobecnosci(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosci.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", nieobecnosc.LekcjaID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", nieobecnosc.UczenID);
            return View(nieobecnosc);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EdytowanieNieobecnosci([Bind(Include = "ID,UczenID,LekcjaID, date")] Nieobecnosc nieobecnosc)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(nieobecnosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", nieobecnosc.LekcjaID);           
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", nieobecnosc.UczenID);
            return View(nieobecnosc);
        }
        
        public ActionResult UsuwanieNieobecnosci(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieobecnosc nieobecnosc = db.Nieobecnosci.Find(id);
            if (nieobecnosc == null)
            {
                return HttpNotFound();
            }
            return View(nieobecnosc);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuwanieNieobecnosciPotwierdzenie(int id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            Nieobecnosc nieobecnosc= db.Nieobecnosci.Find(id);
            db.Nieobecnosci.Remove(nieobecnosc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DodawanieSpoznienia()
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID");
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName");
            return View();
        }

        // POST: Ocena/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodawanieSpoznienia([Bind(Include = "ID,UczenID,LekcjaID, date")] Spoznienie spoznienie)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Spoznienia.Add(spoznienie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", spoznienie.LekcjaID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", spoznienie.UczenID);
            return View(spoznienie);
        }

        // GET: Ocena/Edit/5
        public ActionResult EdytowanieSpoznienia(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spoznienie spoznienie = db.Spoznienia.Find(id);
            if (spoznienie == null)
            {
                return HttpNotFound();
            }
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", spoznienie.LekcjaID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", spoznienie.UczenID);
            return View(spoznienie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EdytowanieSpoznienia([Bind(Include = "ID,UczenID,LekcjaID, date")] Spoznienie spoznienie)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(spoznienie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", spoznienie.LekcjaID);
            ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", spoznienie.UczenID);
            return View(spoznienie);
        }

        public ActionResult UsuwanieSpoznienia(int? id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spoznienie spoznienie = db.Spoznienia.Find(id);
            if (spoznienie == null)
            {
                return HttpNotFound();
            }
            return View(spoznienie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuwaniSpoznieniaPotwierdzenie(int id)
        {
            if (Session["Status"] != "Admin" && Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            Spoznienie spoznienie = db.Spoznienia.Find(id);
            db.Spoznienia.Remove(spoznienie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private IEnumerable<Nieobecnosc> GetNieobecnosciModel(int id)
        {
            var nieobecn = from s in db.Nieobecnosci
                           select s;
            nieobecn = nieobecn.Where(s => s.UczenID == id);
            return nieobecn.AsEnumerable();
        }

        private IEnumerable<Spoznienie> GetSpoznieniaModel(int id)
        {
            var spoznienia = from s in db.Spoznienia
                             select s;
            spoznienia = spoznienia.Where(s => s.UczenID == id);
            return spoznienia.AsEnumerable();
        }

        public ActionResult Test(int? id)
        {
            if (Session["Status"] != "Uczeń")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }/*

            if((Int32)Session["test_count"] == 0) { 
            var pytania = db.Pytania.Where(s => s.TestID == id);

            Session["test_count"] = id.ToString();
            */


            if (Session["test"] != "start")
            {
                var pytania = db.Pytania.Where(s => s.TestID == id).ToArray();
                Session["test"] = "start";
                Session["iter"] = pytania[0].ID;

                return RedirectToAction("Pytanie", "Uczen");







            }


            return View();





        }

        public ActionResult Pytanie()
        {
            if (Session["Status"] != "Uczeń" && Session["test"] != "start")
                return RedirectToAction("Index", "Home");

            if (Session["iter"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.next = true;
            ViewBag.back = true;
            Pytanie pytanie = db.Pytania.Find(Session["iter"]);

            var pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();
            if(pytania[0].ID == pytanie.ID)
                ViewBag.back = false;
            if (pytania[pytania.Count()-1].ID == pytanie.ID)
                ViewBag.next = false;


            ViewBag.title = "Pytanie 1 z " + pytania.Count();
            return View(pytanie);
        }

        [HttpPost, ActionName("Pytanie")]
        [ValidateAntiForgeryToken]
        public ActionResult Pytani(string button)
        {
            ViewBag.back = true;
            ViewBag.next = true;

            Pytanie pytanie = db.Pytania.Find(Session["iter"]);
            /*ViewBag.ans = "zle";
            switch (dec)
            {
                case "1":
                    if (pytanie.odp == odp.odp1)
                        ViewBag.ans = "ok";
                    break;
                case "2":
                    if (pytanie.odp == odp.odp2)
                        ViewBag.ans = "ok";
                    break;
                case "3":
                    if (pytanie.odp == odp.odp3)
                        ViewBag.ans = "ok";
                    break;
                case "4":
                    if (pytanie.odp == odp.odp4)
                        ViewBag.ans = "ok";
                    break;*/
            var pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();

            switch (button) {

                case "Dalej":
                    foreach(Pytanie a in pytania)
                    {
                        if (a.ID > (int)Session["iter"])
                        {
                            Session["iter"] = a.ID;
                            if(pytania[pytania.Count()-1].ID == a.ID)
                            
                                ViewBag.next = false;
                                

                            

                            break;
                        }

                    }


                    break;

                case "Wróć":
                    pytania.Reverse();
                    foreach (Pytanie a in pytania)
                    {
                        if (a.ID < (int)Session["iter"])
                        {
                            Session["iter"] = a.ID;
                            if (pytania[pytania.Count() - 1].ID == a.ID)

                                ViewBag.back = false;
                           

                            break;
                        }

                    }


                    break;
            }
            //
            Pytanie pytanie_next = db.Pytania.Find(Session["iter"]);
            pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();
            int x = 0;
            foreach(Pytanie a in pytania)
            {
                x++;
                if (a.ID == (int)Session["iter"])
                    break;
            }
            ViewBag.title = "Pytanie " + x + " z " + pytania.Count();



            return View(pytanie_next);
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
