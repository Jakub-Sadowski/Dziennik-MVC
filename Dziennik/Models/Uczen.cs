using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Uczen 

    {
        public int ID { get; set; }
        
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }

        public int? KlasaID { get; set; }
        public int? RodzicID { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Spoznienie> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }
        public virtual ICollection<Testy_ucznia> Testy { get; set; }


        public virtual Klasa Klasa { get; set; }
        public virtual Rodzic Rodzic { get; set; }


    }

}