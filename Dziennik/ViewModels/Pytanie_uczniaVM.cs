using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.ViewModels
{
				public class Pytanie_uczniaVM
				{
								public Pytanie_ucznia Pytanie_ucznia { get; set; }
								public IEnumerable<Nauczyciel> Nauczyciele { get; set; }
								public IEnumerable<Przedmiot> Przedmioty { get; set; }

								public Pytanie_uczniaVM(Pytanie_ucznia pytanie_ucznia, IEnumerable<Nauczyciel> nauczyciele, IEnumerable<Przedmiot> przedmioty)
								{
												Pytanie_ucznia = pytanie_ucznia;
												Nauczyciele = nauczyciele;
												Przedmioty = przedmioty;
								}
				}
}