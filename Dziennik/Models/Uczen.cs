using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dziennik.Models
{
    public class Uczen 

    {
        public int ID { get; set; }
        
        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Haslo { get; set; }

        public int? KlasaID { get; set; }
        public int? RodzicID { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Spoznienie> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }
        public virtual ICollection<Testy_ucznia> Testy { get; set; }


        public virtual Klasa Klasa { get; set; }
        public virtual Rodzic Rodzic { get; set; }

								[DisplayName("Uczeń")]
								public string FullName
        {
            get
            {
                return Imie + " " + Nazwisko;
            }
        }


    }

}