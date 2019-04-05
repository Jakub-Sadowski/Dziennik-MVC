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
    }
}

