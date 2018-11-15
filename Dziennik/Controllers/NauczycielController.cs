using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dziennik.DAL;
using Dziennik.Models;
using System.Configuration;
using System.Data.SqlClient;
namespace Dziennik.Controllers
{
    public class NauczycielController : Controller
    {
        private Context db = new Context();

        // GET: Nauczyciel
        public ActionResult Index()
        {
            var nauczyciele = db.Nauczyciele.Include(n => n.Klasa);
            return View(nauczyciele.ToList());
        }

        // GET: Nauczyciel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Tworzenie dynamicznego modelu
            dynamic model = new ExpandoObject();
            model.Nauczyciel = GetNauczyciel();
            //model.Uczen = GetUczniowie();
            //model.Ogloszenie_dla_rodzicow = GetOgloszenia_dla_rodzica();
            //Lista o Nauczycielu 
            List<Nauczyciel> GetNauczyciel()
            {
                List<Nauczyciel> nauczyciel = new List<Nauczyciel>();
                //int? identyfikator = id;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Context"].ConnectionString;

                //string constr = ConfigurationManager.ConnectionStrings["Data Source=(LocalDb;Initial Catalog=Dziennik;Integrated Security=SSPI;"];
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM dbo.Nauczyciel WHERE dbo.Nauczyciel.ID = @id";
                    SqlCommand cmd = new SqlCommand(query);
                    //Specyfikacja przekazanego parametru do query
                    cmd.Parameters.AddWithValue("@id", id);
                    using (cmd)
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                nauczyciel.Add(new Nauczyciel
                                {
                                    NauczycielID = Convert.ToInt32(sdr["ID"]),
                                    imie = sdr["imie"].ToString(),
                                    nazwisko = sdr["nazwisko"].ToString(),
                                    

                                });
                            }
                        }
                        con.Close();
                        return nauczyciel;
                    }
                }
            }
            return View(model);
        }

        // GET: Nauczyciel/Create
        public ActionResult Create()
        {
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            return View();
        }

        // POST: Nauczyciel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo,KlasaID")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Nauczyciele.Add(nauczyciel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", nauczyciel.KlasaID);
            return View(nauczyciel);
        }

        // GET: Nauczyciel/Edit/5
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
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", nauczyciel.KlasaID);
            return View(nauczyciel);
        }

        // POST: Nauczyciel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NauczycielID,imie,nazwisko,login,haslo,KlasaID")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nauczyciel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", nauczyciel.KlasaID);
            return View(nauczyciel);
        }

        // GET: Nauczyciel/Delete/5
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

        // POST: Nauczyciel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nauczyciel nauczyciel = db.Nauczyciele.Find(id);
            db.Nauczyciele.Remove(nauczyciel);
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
