using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Zapytanie
    {
        public int ID { get; set; }
        public int? NauczycielID { get; set; }

        public int? RodzicID { get; set; }
        public string pytanie { get; set; }
        public string odpowiedz { get; set; }
       
        public DateTime data_pytania { get; set; }
        public Nullable<DateTime> data_odpowiedz { get; set; }


        public virtual Nauczyciel Nauczyciel { get; set; }
        public virtual Rodzic Rodzic { get; set; }

    }
}