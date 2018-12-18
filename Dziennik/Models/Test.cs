using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Test
    {
        public int ID { get; set; }
        public int? PrzedmiotID { get; set; }
        public int? KlasaID { get; set; }
        public int? NauczycielID { get; set; }
        public int czasTrwania { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Klasa Klasa { get; set; }
        public virtual Nauczyciel Nauczyciel { get; set; }

        public virtual ICollection<Testy_ucznia> Testy { get; set; }
        public virtual ICollection<Pytanie> Pytania { get; set; }

    }
}