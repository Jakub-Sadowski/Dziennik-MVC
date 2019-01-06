using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public enum dzien
    {
        poniedziałek, wtorek, środa, czwartek, piątek
    }

    public class Lekcja
    {
        public int ID { get; set; }
        public int? NauczycielID { get; set; }
        public int? KlasaID { get; set; }
        public int? PrzedmiotID { get; set; }
        public int godzina { get; set; }
        public dzien? dzien { get; set; }

        public virtual Klasa Klasa { get; set; }
        public virtual Nauczyciel Nauczyciel{ get; set; }
        public virtual Przedmiot Przedmiot { get; set; }

        public virtual ICollection<Spoznienie> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }
    }
}