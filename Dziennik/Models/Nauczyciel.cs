using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Nauczyciel
    {
        public int? NauczycielID { get; set; }
        
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }

        public int? KlasaID { get; set; }
        public virtual ICollection<PrzedmiotKlasaNauczyciel> Link { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Test> Testy { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Ogloszenie> Ogloszenia { get; set; }
        public virtual ICollection<Ogloszenie_dla_rodzicow>Ogloszenia_r { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual Klasa Klasa { get; set; }
    }
}