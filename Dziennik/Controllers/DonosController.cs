using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            if ((string)Session["Status"] != "Uczen")
                return RedirectToAction("Index", "Home");
            var donos = db.Donos.Include(d => d.Nauczyciel).Include(d => d.Uczen);
            return View(donos.ToList());
        }

        // GET: Donos/Details/5
      /*  public ActionResult Details(int? id)
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
        */
        // GET: Donos/Create
        public ActionResult Create()
        {
            if ((string)Session["Status"] != "Uczen")
                return RedirectToAction("Index", "Home");
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "Imie");
            //ViewBag.RodzicID = new SelectList(db.Rodzics, "ID", "Imie");
            return View();
        }

        // POST: Donos/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NauczycielID,RodzicID,pytanie,odpowiedz,data_pytania,data_odpowiedz")] Donos donos)
        {
            if ((string)Session["Status"] != "Uczen")
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Donos.Add(donos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "Imie", donos.NauczycielID);
            donos.data_pytania = DateTime.Now;
            var user = Session["UserID"];
            string ide = user.ToString();
            int id = Convert.ToInt32(ide);
            donos.UczenID = id;
            donos.Uczen = db.Uczniowie.Find(id);

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(donos.Nauczyciel.Email));  // replace with valid value 
            message.From = new MailAddress("mojagracv@gmail.com");  // replace with valid value
            message.Subject = "Wazna wiadomosc od "+donos.Uczen;
            message.Body = string.Format(body, donos.Uczen.FullName,donos.data_pytania, donos.wiadomosc);
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
                smtp.SendMailAsync(message);
              
            }
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
            ViewBag.NauczycielID = new SelectList(db.Nauczyciele, "NauczycielID", "Imie", donos.NauczycielID);
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
            ViewBag.NauczycielID = new SelectList(db.Nauczyciels, "NauczycielID", "Imie", donos.NauczycielID);
            ViewBag.RodzicID = new SelectList(db.Rodzics, "ID", "Imie", donos.RodzicID);
            return View(donos);
        }
        */
        // GET: Donos/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((string)Session["Status"] != "Uczen")
                return RedirectToAction("Index", "Home");
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
            if ((string)Session["Status"] != "Uczen")
                return RedirectToAction("Index", "Home");
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
