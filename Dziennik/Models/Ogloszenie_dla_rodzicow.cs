using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Ogloszenie_dla_rodzicow
    {
        public int ID { get; set; }
        public int? NauczycielID { get; set; }
        public int? RodzicID { get; set; }
        public string naglowek { get; set; }
        public string tresc { get; set; }
        public DateTime data { get; set; }

        public virtual Nauczyciel Nauczyciel { get; set; }
       public virtual Rodzic Rodzic { get; set; }
    }
}