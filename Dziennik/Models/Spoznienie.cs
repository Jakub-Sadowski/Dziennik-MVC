using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Spoznienie
    {
        public int ID { get; set; }
        public int? UczenID { get; set; }
        public int? LekcjaID { get; set; }
        public DateTime date { get; set; }


        public virtual Uczen Uczen { get; set; }
        public virtual Lekcja Lekcja { get; set; }

    }
}