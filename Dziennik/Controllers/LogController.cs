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

        // GET: Log
        public ActionResult Index()
        {
            var viewModel = new Profil
            {
                Uczniowie = db.Uczniowie.Include(u => u.Klasa).Include(u => u.Rodzic).ToList(),
                Rodzice = db.Rodzice.ToList(),
                Administratorzy = db.Administratorzy.ToList(),
                Nauczyciele = db.Nauczyciele.Include(u => u.Klasa).ToList(),
            };
            return View(viewModel);
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
