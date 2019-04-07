using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Donos
    {
        public int ID { get; set; }
        public int? NauczycielID { get; set; }

        public int? UczenID { get; set; }
        public string wiadomosc { get; set; }
       
        public DateTime data_pytania { get; set; }

        public virtual Nauczyciel Nauczyciel { get; set; }
        public virtual Uczen Uczen { get; set; }

    }
}