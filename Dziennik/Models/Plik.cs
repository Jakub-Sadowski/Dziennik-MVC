using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Plik
    {
        public int ID { get; set; }
        [NotMapped]
        public FileStream plik { get; set; }
        public DateTime data { get; set; }

        public int? PrzedmiotID { get; set; }
        public int? KlasaID { get; set; }
        public int? NauczycielID { get; set; }
        

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Klasa Klasa { get; set; }
        public virtual Nauczyciel Nauczyciel { get; set; }

        
    }
}