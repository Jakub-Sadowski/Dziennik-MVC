using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Dziennik.DAL;
using System.Data.Entity;
using Context = Dziennik.DAL.Context;

namespace Dziennik.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        // GET: Ogloszenie
        public ActionResult Index()
        {
            var ogloszenia = db.Ogloszenia.Include(o => o.Nauczyciel);
            return View(ogloszenia.ToList());
        }
    }
}

