using Dziennik.DAL;
using Dziennik.Helpers;
using Dziennik.Models;
using Dziennik.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dziennik.Controllers
{//
	public class UczenController : Controller
	{
		private Context db = new Context();

		#region crud
		public ActionResult Index(string search)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			var uczniowie = from s in db.Uczniowie
							select s;
			if (!String.IsNullOrEmpty(search))
			{
				uczniowie = uczniowie.Where(s => (s.Imie + " " + s.Nazwisko).Contains(search));
			}
			uczniowie = uczniowie.OrderByDescending(s => s.Nazwisko);
			return View(uczniowie.ToList());
		}

		public ActionResult Details(int? id)
		{
			if ((string)Session["Status"] != "Admin")
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

		public ActionResult Create()
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
			ViewBag.RodzicID = new SelectList(db.Rodzice, "ID", "FullName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ID,Imie,Nazwisko,Login,Haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.Login == uczen.Login).ToList();
			if (uczeniowie.Count != 0)
			{
				ModelState.AddModelError("", "Podany Login istnieje w bazie.");
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
			if ((string)Session["Status"] != "Admin")
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
		public ActionResult Edit([Bind(Include = "ID,Imie,Nazwisko,Login,Haslo,KlasaID,RodzicID")] Uczen uczen)
		{
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			List<Uczen> uczeniowie = db.Uczniowie.Where(a => a.Login == uczen.Login).ToList();
			if (uczeniowie.Count != 0)
			{
				ModelState.AddModelError("", "Podany Login istnieje w bazie.");
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
			if ((string)Session["Status"] != "Admin")
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
			if ((string)Session["Status"] != "Admin")
				return RedirectToAction("Index", "Home");

			Uczen uczen = db.Uczniowie.Find(id);
			db.Uczniowie.Remove(uczen);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		#endregion

		public ActionResult Oceny(int? id)
		{

			if ((string)Session["Status"] == "Uczen")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				id = Convert.ToInt32(ide);

			}
			if ((string)Session["Status"] == "Rodzic")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				id = Convert.ToInt32(ide);
				//Console.WriteLine(id);
				var dzieci_rodzica = from s in db.Uczniowie
									 select s;
				dzieci_rodzica = dzieci_rodzica.Where(s => s.RodzicID == id);
				return View(dzieci_rodzica);

			}
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var oceny = from s in db.Oceny
						select s;
			oceny = oceny.Where(s => s.UczenID == id);




			if (oceny == null)
			{
				return HttpNotFound();
			}

			return View(model: oceny);
		}
		[HttpPost, ActionName("Oceny")]
		[ValidateAntiForgeryToken]
		public ActionResult Oceny(int id)
		{
			if ((string)Session["Status"] == "Uczen")
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
			if ((string)Session["Status"] != "Uczen")
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
				.Select(l => l.Przedmiot).Distinct().ToList();
			foreach (var p in przedmioty)
			{
				p.Tresc_ksztalcenia.plikSciezka = FileHandler.GetFileName(p.Tresc_ksztalcenia.plikSciezka);
			}

			return View(przedmioty);
		}

		public ActionResult SzczegolyPrzedmiotu(int? id)
		{
			if ((string)Session["Status"] != "Uczen")
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
								.SingleOrDefault();
			przedmiot.Testy = przedmiot.Testy.Where(t => t.KlasaID == klasa.KlasaID).ToList();

			foreach (var file in przedmiot.Pliki)
			{
				file.FilePath = FileHandler.GetFileName(file.FilePath);
			}

			return View(przedmiot);
		}


		public ActionResult Absencja(int? id)
		{
			if ((string)Session["Status"] == "Uczen")
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

			if ((string)Session["Status"] == "Uczen")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");
			ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID");
			ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DodawanieNieobecnosci([Bind(Include = "ID,UczenID,LekcjaID, date")] Nieobecnosc nieobecnosc)
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
		public ActionResult EdycjaProfilu()
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");
			var id = Convert.ToInt32(Session["UserID"]);
			Uczen rodzic = db.Uczniowie.Find(id);
			ViewBag.Imie = rodzic.Imie;
			ViewBag.Nazwisko = rodzic.Nazwisko;

			return View();
			/*
*   Spoznienie spoznienie = db.Spoznienia.Find(id);
if (spoznienie == null)
{
return HttpNotFound();
}
ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID", spoznienie.LekcjaID);
ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName", spoznienie.UczenID);
return View(spoznienie);
* */
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EdycjaProfilu([Bind(Include = "ID, Imie, Nazwisko")]Uczen userprofile)
		{
			//XDDDDDDDDDDDDDDDDDDDDD
			// if (ModelState.IsValid)
			// {
			int? id = userprofile.ID;
			// Get the userprofile
			Uczen user = db.Uczniowie.FirstOrDefault(u => u.ID == id);

			// Update fields
			user.ID = userprofile.ID;
			user.Imie = userprofile.Imie;
			user.Nazwisko = userprofile.Nazwisko;

			db.Entry(user).State = EntityState.Modified;

			db.SaveChanges();

			// return RedirectToAction("Index", "Home"); // or whatever
			// }

			return View(userprofile);
		}

		#region nieobecności
		public ActionResult EdytowanieNieobecnosci(int? id)
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
		public ActionResult EdytowanieNieobecnosci([Bind(Include = "ID,UczenID,LekcjaID, date, Status")] Nieobecnosc nieobecnosc)
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");
			Nieobecnosc nieobecnosc = db.Nieobecnosci.Find(id);
			db.Nieobecnosci.Remove(nieobecnosc);
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
		#endregion

		public ActionResult IstniejacyTest()
		{

			return View();
		}

		#region spoznienia
		public ActionResult DodawanieSpoznienia()
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");
			ViewBag.LekcjaID = new SelectList(db.Lekcja, "ID", "PrzedmiotID");
			ViewBag.UczenID = new SelectList(db.Uczniowie, "ID", "FullName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DodawanieSpoznienia([Bind(Include = "ID,UczenID,LekcjaID, date")] Spoznienie spoznienie)
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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

		public ActionResult EdytowanieSpoznienia(int? id)
		{
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
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
			if ((string)Session["Status"] != "Admin" && (string)Session["Status"] != "Nauczyciel")
				return RedirectToAction("Index", "Home");
			Spoznienie spoznienie = db.Spoznienia.Find(id);
			db.Spoznienia.Remove(spoznienie);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		private IEnumerable<Spoznienie> GetSpoznieniaModel(int id)
		{
			var spoznienia = from s in db.Spoznienia
							 select s;
			spoznienia = spoznienia.Where(s => s.UczenID == id);
			return spoznienia.AsEnumerable();
		}
		#endregion

		public ActionResult Uwagi(int? id)
		{
			if ((string)Session["Status"] == "Uczen")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				id = Convert.ToInt32(ide);
			}
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var uwagi = from s in db.Uwagi
						select s;
			uwagi = uwagi.Where(s => s.UczenID == id);

			if (uwagi == null)
			{
				return HttpNotFound();
			}

			return View(uwagi);
		}
		[HttpPost, ActionName("Uwagi")]
		[ValidateAntiForgeryToken]
		public ActionResult Uwagi(int id)
		{
			if ((string)Session["Status"] == "Uczen")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				int id1 = Convert.ToInt32(ide);
				var uwagi = from s in db.Uwagi
							select s;
				uwagi = uwagi.Where(s => s.UczenID == id1);
				uwagi = uwagi.Include(u => u.Nauczyciel).Include(u => u.Uczen);
				return View(uwagi.ToList());
			}
			else
			{
				var uwagi = from s in db.Uwagi
							select s;
				uwagi = uwagi.Where(s => s.UczenID == id);
				uwagi = uwagi.Include(u => u.Nauczyciel).Include(u => u.Uczen);
				return View(uwagi.ToList());
			}
		}

		#region elearningTesty
		public ActionResult Test(int? id)
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");

			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var user = Session["UserID"];
			string ide = user.ToString();
			int id_ucznia = Convert.ToInt32(ide);
			var test = db.Testy.Find(id);
			var testy = db.Testy_ucznia.Where(s => s.UczenID == id_ucznia).Where(s => s.TestID == id);

			if (testy.Count() != 0)
				return RedirectToAction("IstniejacyTest", "Uczen");

			var pytania = db.Pytania.Where(s => s.TestID == id).ToArray();
			Session["test"] = "start";
			Session["testID"] = id;
			Session["pozostalyCzasTestu"] = test.czasTrwania;
			Session["iter"] = pytania[0].ID;
			int[] cache = new int[pytania.Count()];
			Session["cache"] = cache;

			return RedirectToAction("Pytanie", "Uczen");
		}

		public ActionResult Pytanie()
		{
			if ((string)Session["Status"] != "Uczen" && (string)Session["test"] != "start")
				return RedirectToAction("Index", "Home");

			if (Session["iter"] == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			ViewBag.next = true;
			ViewBag.back = true;
			Pytanie pytanie = db.Pytania.Find(Session["iter"]);

			var pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();
			if (pytania[0].ID == pytanie.ID)
				ViewBag.back = false;
			if (pytania[pytania.Count() - 1].ID == pytanie.ID)
				ViewBag.next = false;

			ViewBag.title = "Pytanie 1 z " + pytania.Count();
			ViewBag.time = (int)Session["pozostalyCzasTestu"];

			var vm = new PytanieVM(pytanie);
			vm.SetPathsToBase64();
			return View(vm);
		}

		[HttpPost, ActionName("Pytanie")]
		[ValidateAntiForgeryToken]
		public ActionResult Pytani(string button, string dec, int pozostalyCzasTestu)
		{
			ViewBag.back = true;
			ViewBag.next = true;

			Pytanie pytanie = db.Pytania.Find(Session["iter"]);

			var pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();
			int[] cache = (int[])Session["cache"];
			int x = 0;
			foreach (Pytanie a in pytania)
			{
				x++;
				if (a.ID == (int)Session["iter"])
					break;
			}
			switch (dec)
			{
				case "1":
					cache[x - 1] = 1;
					break;
				case "2":
					cache[x - 1] = 2;
					break;
				case "3":
					cache[x - 1] = 3;
					break;
				case "4":
					cache[x - 1] = 4;
					break;

			}
			Session["cache"] = cache;

			switch (button)
			{
				case "Dalej":
					foreach (Pytanie a in pytania)
					{
						if (a.ID > (int)Session["iter"])
						{
							Session["iter"] = a.ID;
							if (pytania[pytania.Count() - 1].ID == a.ID)
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
				case "Zapisz test":
					int wynik = 0;
					int max = 0;
					for (int i = 0; i < cache.Length; i++)
					{
						max += pytania[i].punktacja;
						if (cache[i] == (int)pytania[i].odp)
							wynik += pytania[i].punktacja;
					}

					Session["wynik"] = wynik;
					Session["max"] = max;
					return RedirectToAction("Wynik");
			}

			Pytanie pytanie_next = db.Pytania.Find(Session["iter"]);
			pytania = db.Pytania.Where(s => s.TestID == pytanie.TestID).ToList();
			x = 0;
			foreach (Pytanie a in pytania)
			{
				x++;
				if (a.ID == (int)Session["iter"])
					break;
			}
			ViewBag.title = "Pytanie " + x + " z " + pytania.Count();
			cache = (int[])Session["cache"];
			ViewBag.cache = cache[x - 1];
			Session["pozostalyCzasTestu"] = pozostalyCzasTestu;
			ViewBag.time = (int)Session["pozostalyCzasTestu"];

			ModelState.Clear();

			var vm = new PytanieVM(pytanie_next);
			vm.SetPathsToBase64();
			return View(vm);
		}

		public ActionResult Wynik()
		{
			if ((string)Session["Status"] != "Uczen" && (string)Session["test"] != "start")
				return RedirectToAction("Index", "Home");
			ViewBag.wynik = Session["wynik"];
			ViewBag.max = (int)Session["max"];
			int testID = (int)Session["testID"];
			int userID = Convert.ToInt32((string)Session["UserID"]);
			int wynik = (int)Session["wynik"];
			float wynik_f = (float)wynik;
			int max = (int)Session["max"];
			float max_f = (float)max;
			double oc = 0;


			float procent = wynik_f / max_f;
			if (procent <= 0.27)
				oc = 1;
			if (procent > 0.27 && procent <= 0.44)
				oc = 2;
			if (procent > 0.44 && procent <= 0.5)
				oc = 2.5;
			if (procent > 0.5 && procent <= 0.65)
				oc = 3;
			if (procent > 0.65 && procent <= 0.71)
				oc = 3.5;
			if (procent > 0.71 && procent <= 0.8)
				oc = 4;
			if (procent > 0.8 && procent <= 0.85)
				oc = 4.5;
			if (procent > 0.85 && procent <= 0.9)
				oc = 5;
			if (procent > 0.9 && procent <= 0.95)
				oc = 5.5;
			if (procent > 0.95 && procent <= 1)
				oc = 6;
			Test test = db.Testy.Find(Session["testID"]);
			db.Oceny.Add(new Ocena
			{
				ocena = oc,
				waga = 1,
				data = DateTime.Now,
				NauczycielID = test.NauczycielID ?? default(int),
				PrzedmiotID = test.PrzedmiotID ?? default(int),
				UczenID = userID,
				tresc = "test"
			});
			db.SaveChanges();

			db.Testy_ucznia.Add(new Testy_ucznia
			{
				TestID = testID,
				UczenID = userID,
				Wynik = (int)wynik
			});
			db.SaveChanges();
			Session.Remove("test");
			Session.Remove("wynik");
			Session.Remove("max");
			Session.Remove("iter");
			Session.Remove("cache");
			ViewBag.ocena = oc;
			return View();
		}
		#endregion

		public ActionResult TestyUcznia(int? id)
		{
			if ((string)Session["Status"] == "Uczen")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				id = Convert.ToInt32(ide);
			}
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var testy = from s in db.Testy_ucznia
						select s;
			testy = testy.Where(s => s.UczenID == id);
			testy = testy.Include(u => u.Test);

			if (testy == null)
			{
				return HttpNotFound();
			}

			return View(testy);
		}
		[HttpPost, ActionName("TestyUcznia")]
		[ValidateAntiForgeryToken]
		public ActionResult TestyUcznia(int id)
		{
			if ((string)Session["Status"] == "Uczen")
			{
				var user = Session["UserID"];
				string ide = user.ToString();
				int id1 = Convert.ToInt32(ide);
				var testy = from s in db.Testy_ucznia
							select s;
				testy = testy.Where(s => s.UczenID == id1);
				testy = testy.Include(u => u.Test);
				return View(testy.ToList());
			}
			else
			{
				var testy = from s in db.Testy_ucznia
							select s;
				testy = testy.Where(s => s.UczenID == id);
				testy = testy.Include(u => u.Test);
				return View(testy.ToList());
			}
		}

		public ActionResult PlanLekcji()
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");

			var userId = Convert.ToInt32(Session["UserID"]);
			var klasa = db.Klasy
							.Include(k => k.Uczniowie)
							.Where(k => k.Uczniowie.Any(u => u.ID == userId))
							.SingleOrDefault();

			var lekcje = db.Lekcja
							.Where(l => l.KlasaID == klasa.KlasaID)
							.ToList();

			return View(lekcje);
		}

		#region Pytanie do nauczyciela
		public ActionResult PytaniaDoNauczyciela()
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");

			var userId = Convert.ToInt32(Session["UserID"]);
			return View(db.Pytania_ucznia.Where(x => x.UczenID == userId).OrderByDescending(x => x.Data_pytania).ToList());
		}

		public ActionResult DodawaniePytaniaDoNauczyciela()
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");

			ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "FullName");
			return View(new Pytanie_uczniaVM(null, db.Nauczyciele, db.Przedmioty));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DodawaniePytaniaDoNauczyciela([Bind(Include = "ID,NauczycielID,UczenID,PrzedmiotID,Pytanie")] Pytanie_ucznia pytanie_ucznia)
		{
			if ((string)Session["Status"] != "Uczen")
				return RedirectToAction("Index", "Home");

			if (ModelState.IsValid)
			{
				var userId = Convert.ToInt32(Session["UserID"]);
				pytanie_ucznia.Data_pytania = DateTime.Now;
				pytanie_ucznia.UczenID = userId;
				db.Pytania_ucznia.Add(pytanie_ucznia);
				db.SaveChanges();

				var u = db.Uczniowie.Find(userId);
				var n = db.Nauczyciele.Find(pytanie_ucznia.NauczycielID);
				string subject;
				if (pytanie_ucznia.PrzedmiotID == null || pytanie_ucznia.PrzedmiotID < 1)
					subject = "Ogólne";
				else
					subject = db.Przedmioty.Find(pytanie_ucznia.PrzedmiotID).nazwa;
				await EmailHelper.Send(n.Email, EmailHelper.APP_EMAIL, $"<pre>Uczen {u.FullName} zadał pytanie.\n{pytanie_ucznia.Pytanie}</pre>", $"Pytanie - {subject}");
				return RedirectToAction("PytaniaDoNauczyciela");
			}

			return View(new Pytanie_uczniaVM(pytanie_ucznia, db.Nauczyciele, db.Przedmioty));
		}

		public ActionResult PytanieDoNauczyciela(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Pytanie_ucznia pytanie_ucznia = db.Pytania_ucznia.Find(id);
			if (pytanie_ucznia == null)
			{
				return HttpNotFound();
			}
			return View(pytanie_ucznia);
		}

		public ActionResult PytanieDoNauczycielaDelete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Pytanie_ucznia pytanie_ucznia = db.Pytania_ucznia.Find(id);
			if (pytanie_ucznia == null)
			{
				return HttpNotFound();
			}
			return View(pytanie_ucznia);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult PytanieDoNauczycielaDelete(int id)
		{
			Pytanie_ucznia pytanie_ucznia = db.Pytania_ucznia.Find(id);
			db.Pytania_ucznia.Remove(pytanie_ucznia);
			db.SaveChanges();
			return RedirectToAction("PytaniaDoNauczyciela");
		}
		#endregion


		public ActionResult Profil(int? id, int? liczba)
		{
			ViewBag.control = liczba;
			if ((string)Session["Status"] != "Uczen" || Int32.Parse((string)Session["UserID"]) != id)
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



		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Zmien_Login(Uczen uczen)
		{
			int liczba;
			if ((string)Session["Status"] != "Uczen" || Int32.Parse((string)Session["UserID"]) != uczen.ID)
				return RedirectToAction("Index", "Home", new { id = "elo" });

			if (uczen == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (db.Administratorzy.Where(a => a.Login == uczen.Login).Count()
				+ db.Uczniowie.Where(a => a.Login == uczen.Login).Count()
				+ db.Rodzice.Where(a => a.Login == uczen.Login).Count()
				+ db.Nauczyciele.Where(a => a.Login == uczen.Login).Count()

				> 0 && db.Uczniowie.Find(uczen.ID).Login != uczen.Login)
				liczba = 1;

			else
			{
				liczba = 0;
				if (ModelState.IsValid)
				{

					db.Uczniowie.AddOrUpdate(uczen);
					db.SaveChanges();

				}

			}

			return RedirectToAction("Profil", "Uczen", new { id = uczen.ID, liczba = liczba });

		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Zmien_haslo(Uczen uczen)
		{

			if ((string)Session["Status"] != "Uczen" || Int32.Parse((string)Session["UserID"]) != uczen.ID)
				return RedirectToAction("Index", "Home");

			if (uczen == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}


			if (ModelState.IsValid)
			{

				db.Uczniowie.AddOrUpdate(uczen);
				db.SaveChanges();

			}



			return RedirectToAction("Profil", "Uczen", new { id = uczen.ID });

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
