﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Dziennik.Helpers
{
    static public class FileHandler
    {
        public static string saveFile(HttpPostedFileBase postedFile)
        {
            var filename = addGuid(postedFile.FileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads"), filename);
            postedFile.SaveAs(path);
            return path;
        }

        public static void deleteFile(string path)
        {
            File.Delete(path);
        }

        static string addGuid(string filename)
        {
            return $"{Guid.NewGuid().ToString()}_{filename}";
        }

        public static string getFileName(string path)
        {
            var fileName = Path.GetFileName(path);
            return removeGuid(fileName);
        }

        public static string removeGuid(string fileName)
        {
            var parts = fileName.Split('_');
            if (parts.Length != 2)
                throw new ArgumentException();
            return parts[1];
        }
    }
}