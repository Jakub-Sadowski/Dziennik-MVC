using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dziennik.Models
{
    public enum kl
    {
        kl1, kl2, kl3
    }
    public class Przedmiot

    {
        public int? ID { get; set; }
        [Display(Name = "Nazwa")][Required] public string nazwa { get; set; }
		[Display(Name = "Poziom")] [Required] public kl level { get; set; }

        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Lekcja> Lekcje { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Test> Testy { get; set; }
		[Display(Name = "Treść ksztalcenia")] public virtual Tresc_ksztalcenia Tresc_ksztalcenia { get; set; }

    }

}