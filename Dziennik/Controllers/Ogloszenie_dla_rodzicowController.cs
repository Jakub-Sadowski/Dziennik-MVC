using Dziennik.DAL;
using Dziennik.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
    public class Ogloszenie_dla_rodzicowController : Controller
    {
        private Context db = new Context();

        // GET: Ogloszenie_dla_rodzicow
        public ActionResult Index()
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            var ogloszenia_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Include(o => o.klasa).Include(o => o.Nauczyciel);
            return View(ogloszenia_dla_rodzicow.ToList());
        }

        // GET: Ogloszenie_dla_rodzicow/Details/5
        public ActionResult Details(int? id)
        {
            if ((string)Session["Status"] != "Nauczyciel" && Session["Status"] != "Rodzic" )
                return RedirectToAction("Index", "Home");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Create
        public ActionResult Create()
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie");
            return View();
        }

        // POST: Ogloszenie_dla_rodzicow/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "ID,NauczycielID,KlasaID,naglowek,tresc,data")] Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow)
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");
            var rodzice = from s in db.Uczniowie
                         where s.KlasaID==ogloszenie_dla_rodzicow.KlasaID
                         select s.Rodzic;
            var emaile =  from b in rodzice
                          from a in db.Rodzice
                          where a==b
                          select a.Email;
       
            if (ModelState.IsValid)
            {
                db.Ogloszenia_dla_rodzicow.Add(ogloszenie_dla_rodzicow);
                ogloszenie_dla_rodzicow.data = DateTime.Now;
                var user = Session["UserID"];
                string ide = user.ToString();
                int id1 = Convert.ToInt32(ide);
                ogloszenie_dla_rodzicow.NauczycielID = id1;
                
                db.SaveChanges();
                var body = ogloszenie_dla_rodzicow.tresc;
                var message = new MailMessage();
                foreach (var item in emaile) {
                    message.To.Add(new MailAddress(item));
                }
                message.From = new MailAddress("mojagracv@gmail.com");
                message.Subject = "Dodano nowe ogłoszenie w dzienniku elektronicznym " + ogloszenie_dla_rodzicow.naglowek;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "mojagracv@gmail.com",  // replace with valid value
                        Password = "civilization96"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
                return RedirectToAction("Index");
            }

            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = Session["UserID"];

            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie_dla_rodzicow.NauczycielID);
            return View(ogloszenie_dla_rodzicow);
        }

        // POST: Ogloszenie_dla_rodzicow/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NauczycielID,KlasaID,naglowek,tresc,data")] Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow)
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                ogloszenie_dla_rodzicow.data = DateTime.Now;
                db.Entry(ogloszenie_dla_rodzicow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlasaID = new SelectList(db.Klasy, "KlasaID", "nazwa", ogloszenie_dla_rodzicow.KlasaID);
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "imie", ogloszenie_dla_rodzicow.NauczycielID);
           
            return View(ogloszenie_dla_rodzicow);
        }

        // GET: Ogloszenie_dla_rodzicow/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            if (ogloszenie_dla_rodzicow == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie_dla_rodzicow);
        }

        // POST: Ogloszenie_dla_rodzicow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((string)Session["Status"] != "Nauczyciel")
                return RedirectToAction("Index", "Home");

            Ogloszenie_dla_rodzicow ogloszenie_dla_rodzicow = db.Ogloszenia_dla_rodzicow.Find(id);
            db.Ogloszenia_dla_rodzicow.Remove(ogloszenie_dla_rodzicow);
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
