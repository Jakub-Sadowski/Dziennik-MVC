﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Nauczyciel
    {
        public int? NauczycielID { get; set; }
        
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public string Email { get; set; }


        public virtual ICollection<Zapytanie> Zapytania { get; set; }
        public virtual ICollection<Klasa> WychowywaneKlasy { get; set; }
        public virtual ICollection<Lekcja> Lekcje { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Test> Testy { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Ogloszenie> Ogloszenia { get; set; }
        public virtual ICollection<Ogloszenie_dla_rodzicow>Ogloszenia_r { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }

        [DisplayName("Nauczyciel")]
        public string FullName
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }
    }
}