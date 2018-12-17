using System.Collections.Generic;

namespace Dziennik.Models
{
    public class Absencja
    {
        public IEnumerable<Nieobecnosc> Nieobecnosci { get; set; }
        public IEnumerable<Spoznienie> Spoznienia { get; set; }
    }
}