using Dziennik.DAL;
using Dziennik.Helpers;
using System;
using System.Web;
using System.Web.Mvc;

namespace Dziennik.Controllers
{
				public class PlikController : Controller
				{
								private Context db = new Context();

								public ActionResult DownloadFile(int? id)
								{
												var plik = db.Pliki.Find(id);
												if (plik == null)
																throw new ArgumentException();
												var path = plik.FilePath;
												byte[] fileBytes = System.IO.File.ReadAllBytes(path);
												string fileName = FileHandler.GetFileName(path);
												var mime = MimeMapping.GetMimeMapping(fileName);
												return File(fileBytes, mime, fileName);
								}
				}
}