using Dziennik.DAL;
using Dziennik.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class OgloszenieController : Controller
    {
        private Context db = new Context();

        // GET: Ogloszenie
        public ActionResult Index()
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            var ogloszenia = db.Ogloszenia.Include(o => o.Nauczyciel);
            return View(ogloszenia.ToList());
        }

        // GET: Ogloszenie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenia.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Create
        public ActionResult Create()
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            ViewBag.NauczycielID = Session["UserID"];
            ViewBag.data = DateTime.Now;


            return View();
        }

        // POST: Ogloszenie/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,naglowek,tresc,data")] Ogloszenie ogloszenie)
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Ogloszenia.Add(ogloszenie);
                ogloszenie.data = DateTime.Now;
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);
                ogloszenie.NauczycielID = id1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = Session["UserID"];
            ViewBag.data = DateTime.Now;
            return View(ogloszenie);

        }

        // GET: Ogloszenie/Edit/5
        public ActionResult Edit(int? id)
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenia.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie.NauczycielID);

            return View(ogloszenie);
        }


        // POST: Ogloszenie/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,naglowek,tresc,data")] Ogloszenie ogloszenie)
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(ogloszenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie.NauczycielID);
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Delete/5
        public ActionResult Delete(int? id)
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenia.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (((string)Session["Status"] != "Nauczyciel") && ((string)Session["Status"] != "Admin"))
                return RedirectToAction("Index", "Home");

            Ogloszenie ogloszenie = db.Ogloszenia.Find(id);
            db.Ogloszenia.Remove(ogloszenie);
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
