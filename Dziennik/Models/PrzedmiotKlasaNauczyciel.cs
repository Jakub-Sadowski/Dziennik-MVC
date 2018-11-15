using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class PrzedmiotKlasaNauczyciel
    {
        public int ID { get; set; }
        public int PrzedmiotID { get; set; }
        public int KlasaID { get; set; }
        public int NauczycielID { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Klasa Klasa{ get; set; }
        public virtual Nauczyciel Nauczyciel{ get; set; }
    }
}