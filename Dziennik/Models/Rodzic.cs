using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Rodzic
    {
        public int ID { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }

        public virtual ICollection<Ogloszenie_dla_rodzicow> Ogloszenia { get; set; }
        public virtual ICollection<Uczen> Uczniowie { get; set; }
    }
}