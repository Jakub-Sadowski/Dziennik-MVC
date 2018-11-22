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
    public class LogController : Controller
    {
        private Context db = new Context();

        public ActionResult Login()
        {
            if(Session["UserName"] != null)
                return RedirectToAction("Gdy_zalogowany");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password)
        {
            if (ModelState.IsValid)
            {
               
                    var administratorzy = db.Administratorzy.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                    if (administratorzy != null)
                    {
                        Session["UserID"] = administratorzy.ID.ToString();
                        Session["UserName"] = administratorzy.login.ToString();
                        Session["Name"] = administratorzy.imie.ToString();
                        Session["Forname"] = administratorzy.nazwisko.ToString();
                        Session["Status"] = "Admin";
                        return RedirectToAction("Zalogowany");
                    }
                var rodzice = db.Rodzice.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (rodzice != null)
                {
                    Session["UserID"] = rodzice.ID.ToString();
                    Session["UserName"] = rodzice.login.ToString();
                    Session["Name"] = rodzice.imie.ToString();
                    Session["Forname"] = rodzice.nazwisko.ToString();
                    Session["Status"] = "Rodzic";
                    return RedirectToAction("Zalogowany");
                }
                var uczniowie = db.Uczniowie.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (uczniowie != null)
                {
                    Session["UserID"] = uczniowie.ID.ToString();
                    Session["UserName"] = uczniowie.login.ToString();
                    Session["Name"] = uczniowie.imie.ToString();
                    Session["Forname"] = uczniowie.nazwisko.ToString();
                    Session["Status"] = "Uczeń";
                    return RedirectToAction("Zalogowany");
                }
                var nauczyciele = db.Nauczyciele.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (nauczyciele != null)
                {
                    Session["UserID"] = nauczyciele.NauczycielID.ToString();
                    Session["UserName"] = nauczyciele.login.ToString();
                    Session["Name"] = nauczyciele.imie.ToString();
                    Session["Forname"] = nauczyciele.nazwisko.ToString();
                    Session["Status"] = "Nauczyciel";
                    return RedirectToAction("Zalogowany");
                }
                ViewBag.message = "Błędny login lub hasło";
            }

            
            return View();
        }
        
        public ActionResult Zalogowany()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

         public ActionResult Wyloguj()
        {
            if (Session["UserID"] != null)
            {
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["Name"] = null;
                Session["Forname"] = null;
                Session["Status"] = null;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Gdy_zalogowany()
        {
            return View();

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
