using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Testy_ucznia
    {
        public int ID { get; set; }
        public int? UczenID { get; set; }
        public int? TestID { get; set; }
        public int Wynik { get; set; }

        public virtual Uczen Uczen { get; set; }
        public virtual Test Test { get; set; }

    }
}