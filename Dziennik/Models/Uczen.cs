using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Uczen 

    {
        public int ID { get; set; }
        
        [Required]
        public string imie { get; set; }

        [DisplayName("Imię rodzica")]
        public string rodzicImie { get; set; }

        [DisplayName("Nazwisko rodzica")]
        public string rodzicNazwisko { get; set; }

        [Required]
        public string nazwisko { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        public string haslo { get; set; }

        public int? KlasaID { get; set; }
        public int? RodzicID { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Spoznienie> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }
        public virtual ICollection<Testy_ucznia> Testy { get; set; }


        public virtual Klasa Klasa { get; set; }
        public virtual Rodzic Rodzic { get; set; }

        public string FullName
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }


    }

}