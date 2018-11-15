using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public enum odp
    {
        odp1,odp2,odp3,odp4
    }

    public class Pytanie
    {
        public int ID { get; set; }
        public int? TestID { get; set; }
        public string tresc { get; set; }
        public string odpowiedz1 { get; set; }
        public string odpowiedz2 { get; set; }
        public string odpowiedz3 { get; set; }
        public string odpowiedz4 { get; set; }
        public int punktacja { get; set; }
        public odp? odp { get; set; }

        public virtual Test Test { get; set; }


    }

}