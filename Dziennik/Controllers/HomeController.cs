using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Context = Dziennik.DAL.Context;

namespace Dziennik.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index()
        {
            var ogloszenia = db.Ogloszenia.Include(o => o.Nauczyciel).OrderByDescending(x => x.data);
            return View(ogloszenia.ToList());
        }

        public ActionResult Profil()
        {

      

                int id = Int32.Parse((string)Session["UserID"]);


                if(Session["Status"] == "Admin")
                    return RedirectToAction("Profil", "Administrator",new {id = id });
                else
                if (Session["Status"] == "Uczen")
                    return RedirectToAction("Profil", "Uczen", new { id = id });
                else
                if (Session["Status"] == "Nauczyciel")
                    return RedirectToAction("Profil", "Nauczyciel", new { id = id });

                else
                if (Session["Status"] == "Rodzic")
                    return RedirectToAction("Profil", "Rodzic", new { id = id });
                else
                    return RedirectToAction("Index", "Home");






        }

    }
}

