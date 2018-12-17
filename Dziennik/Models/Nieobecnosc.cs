using System;

namespace Dziennik.Models
{
    public class Nieobecnosc
    {
        public enum status
        {
            Nieusprawiedliwiona, Usprawiedliwiona
        }

        public int ID { get; set; }
        public int? UczenID { get; set; }
        public int? LekcjaID { get; set; }
        public DateTime date{ get; set; }
        public status? Status { get; set; }


        public virtual Uczen Uczen { get; set; }
        public virtual Lekcja Lekcja { get; set; }
    }
}