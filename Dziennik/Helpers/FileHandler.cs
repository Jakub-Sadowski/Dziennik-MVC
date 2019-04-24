using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Dziennik.Helpers
{
    static public class FileHandler
    {
        public static string SaveFile(HttpPostedFileBase postedFile)
        {
            var filename = AddGuid(postedFile.FileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
            postedFile.SaveAs(path);
            return filename;
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        static string AddGuid(string filename)
        {
            return $"{Guid.NewGuid().ToString()}_{filename}";
        }

        public static string GetFileName(string path)
        {
            var fileName = Path.GetFileName(path);
            return RemoveGuid(fileName);
        }

								public static string GetAbsolutePath(string filename)
								{
												return Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
								}

        public static string RemoveGuid(string fileName)
        {
            var start = fileName.IndexOf('_');
												//mozliwe ze kilka przedmiotow ma tą samą treść kształecnia ustawioną (mimo że to nie ma sensu)
												//i wtedy dwa razy z jednego obiektu usunie guid przy czym przy drugim sie wywaliłoby
												if (start == -1)
																return fileName;
            start++;
            var name = fileName.Substring(start, fileName.Length - start);
            return name;
        }

								public static string  GetBase64(string filename)
								{
												var path = FileHandler.GetAbsolutePath(filename);
												byte[] fileBytes = File.ReadAllBytes(path);
												string fileName = FileHandler.GetFileName(path);
												var mime = MimeMapping.GetMimeMapping(fileName);
												return $"data:{mime};base64,{Convert.ToBase64String(fileBytes)}";
								}
    }
}