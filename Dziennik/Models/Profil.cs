using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Profil
    {
        public List<Administrator> Administratorzy { get; set; }
        public List<Uczen> Uczniowie { get; set; }
        public List<Rodzic> Rodzice { get; set; }
        public List<Nauczyciel> Nauczyciele { get; set; }
        public List<Ogloszenie_dla_rodzicow> OgloszeniaR { get; set; }
    }
}