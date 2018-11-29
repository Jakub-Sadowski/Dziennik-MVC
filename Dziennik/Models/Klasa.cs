using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Dziennik.Models
{
    public enum klasa
    {
        kl1,kl2,kl3
    }
    public class Klasa
    {
        
        public int? KlasaID { get; set; }
        
        public string nazwa { get; set; }
      
        public klasa? level { get; set; }

        [ForeignKey("Wychowawca")]
        public int? WychowawcaID { get; set; }
        public virtual Nauczyciel Wychowawca{ get; set; }

        public virtual ICollection<Uczen> Uczniowie { get; set; }
        public virtual ICollection<Lekcja> Lekcje { get; set; }
        public virtual ICollection<Plik> Pliki{ get; set; }
        public virtual ICollection<Test> Testy { get; set; }

        [DisplayName("Klasa")]
        public string Name { get
            {
                return $"{nazwa} {level.Value.ToString()}";
            } }

    }
}