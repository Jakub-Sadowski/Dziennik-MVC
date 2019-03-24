using Dziennik.DAL;
using Dziennik.Helpers;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Login(string login, string password, bool rememberme)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Administratorzy.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (admin != null)
                {
                    SetSessionAndCookies(
                        admin.ID.ToString(), admin.login.ToString(), admin.imie.ToString(),
                        admin.nazwisko.ToString(), "Admin", rememberme);
                    return RedirectToAction("Zalogowany");
                }
                var rodzic = db.Rodzice.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (rodzic != null)
                {
                    SetSessionAndCookies(
                            rodzic.ID.ToString(), rodzic.login.ToString(), rodzic.imie.ToString(),
                            rodzic.nazwisko.ToString(), "Rodzic", rememberme);
                    return RedirectToAction("Zalogowany");
                }
                var uczen = db.Uczniowie.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (uczen != null)
                {
                    SetSessionAndCookies(
                            uczen.ID.ToString(), uczen.login.ToString(), uczen.imie.ToString(),
                            uczen.nazwisko.ToString(), "Uczen", rememberme);
                    return RedirectToAction("Zalogowany");
                }
                var nauczyciel = db.Nauczyciele.Where(a => a.login.Equals(login) && a.haslo.Equals(password)).FirstOrDefault();
                if (nauczyciel != null)
                {
                    SetSessionAndCookies(
                            nauczyciel.NauczycielID.ToString(), nauczyciel.login.ToString(), nauczyciel.imie.ToString(),
                            nauczyciel.nazwisko.ToString(), "Nauczyciel", rememberme);
                    return RedirectToAction("Pytania_rodzicow","Nauczyciel");
                }
                ViewBag.message = "Błędny login lub hasło";
            }

            
            return View();
        }

        private void SetSessionAndCookies(string userID, string userName, string name, string forname, string status, bool appendCookies)
        {
            Session["UserID"] = userID;
            Session["UserName"] = userName;
            Session["Name"] = name;
            Session["Forname"] = forname;
            Session["Status"] = status;

            if (appendCookies)
                AppendCookies(userID, userName, name, forname, status);
            else
                ExpireCookies();
        }

        private void AppendCookies(string userID, string userName, string name, string forname, string status)
        {
            var id = int.Parse(userID);
            Response.AppendCookie(new HttpCookie("1", CookieEncryption.Encrypt(id).ToString()));
            Response.AppendCookie(new HttpCookie("2", userName));
            Response.AppendCookie(new HttpCookie("3", name));
            Response.AppendCookie(new HttpCookie("4", forname));
            Response.AppendCookie(new HttpCookie("5", CookieEncryption.Encrypt(status)));
        }

        private void ExpireCookies()
        {
            Response.Cookies["1"].Expires = DateTime.MinValue;
            Response.Cookies["2"].Expires = DateTime.MinValue;
            Response.Cookies["3"].Expires = DateTime.MinValue;
            Response.Cookies["4"].Expires = DateTime.MinValue;
            Response.Cookies["5"].Expires = DateTime.MinValue;
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
                SetSessionAndCookies(null, null, null, null, null, false);
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
