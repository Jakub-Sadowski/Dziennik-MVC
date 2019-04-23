using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public enum odp
    {
        odp1 = 1,
								odp2 = 2,
								odp3 = 3,
								odp4 = 4
    }

				public class Pytanie
    {
        public int ID { get; set; }
        public int? TestID { get; set; }
        [DisplayName("Treść pytania")]public string tresc { get; set; }
        public string odpowiedz1 { get; set; }
        public string odpowiedz2 { get; set; }
        public string odpowiedz3 { get; set; }
        public string odpowiedz4 { get; set; }
        public int punktacja { get; set; }
        public odp? odp { get; set; }

								public virtual List<Multimedia> Multimedia { get; set; }
								public virtual Test Test { get; set; }


    }

}