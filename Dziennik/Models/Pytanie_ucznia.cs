using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
				public class Pytanie_ucznia
				{
								public int ID { get; set; }
								public int? NauczycielID { get; set; }
								public int? UczenID { get; set; }
								public int? PrzedmiotID { get; set; }

								[DataType(DataType.MultilineText)]
								[Display(Name = "Pytanie")]
								public string Pytanie { get; set; }

								[DataType(DataType.MultilineText)]
								[Display(Name = "Odpowiedź")]
								public string Odpowiedz { get; set; }

								[Display(Name = "Zadano")]
								public DateTime Data_pytania { get; set; }
								[Display(Name = "Odpowiedziano")]
								public DateTime? Data_odpowiedzi { get; set; }

								public virtual Nauczyciel Nauczyciel { get; set; }
								public virtual Uczen Uczen { get; set; }
								public virtual Przedmiot Przedmiot{ get; set; }
				}
}