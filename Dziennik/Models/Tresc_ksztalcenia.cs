using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Dziennik.Models
{
    public class Tresc_ksztalcenia
    {
        
        [Key]
        [ForeignKey("Przedmiot")]
        public int? PrzedmiotID { get; set; }
        [Required] public string plikSciezka { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        

    }

}