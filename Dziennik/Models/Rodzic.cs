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
        public string imie { get; set; }

        [Required]
        public string nazwisko { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        public string haslo { get; set; }

        public virtual ICollection<Ogloszenie_dla_rodzicow> Ogloszenia { get; set; }
        public virtual ICollection<Uczen> Uczniowie { get; set; }
        public string FullName
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }
    }
}