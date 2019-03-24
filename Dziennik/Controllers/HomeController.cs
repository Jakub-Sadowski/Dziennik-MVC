using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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

