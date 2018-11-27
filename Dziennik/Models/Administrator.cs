using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Administrator

    {
        public int ID { get; set; }
        [Required]
        public string imie { get; set; }
        [Required]
        public string nazwisko { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string haslo { get; set; }
        public string FullName
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }
    }
}