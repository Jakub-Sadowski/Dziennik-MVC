using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Rodzic
    {
        public int? ID { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Haslo { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Ogloszenie_dla_rodzicow> Ogloszenia { get; set; }
        public virtual ICollection<Zapytanie> Zapytania { get; set; }
        public virtual ICollection<Uczen> Uczniowie { get; set; }
        public string FullName
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }
    }
}