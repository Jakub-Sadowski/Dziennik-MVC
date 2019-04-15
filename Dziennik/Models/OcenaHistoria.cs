using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
				public class OcenaHistoria
				{
								public int ID { get; set; }
								[DisplayName("Ocena")]
								public double ocena { get; set; }
								[DisplayName("Waga")]
								public int waga { get; set; }
								[DisplayName("Data wystawienia")]
								public DateTime data { get; set; }
								[DisplayName("Notatka")]
								public string tresc { get; set; }
								public int PrzedmiotID { get; set; }
								public int NauczycielID { get; set; }
								public int UczenID { get; set; }
								public int OcenaID { get; set; }
								public int? IdEdytujacego { get; set; }
								[DisplayName("Data zmiany")]
								public DateTime? dataEdycji { get; set; }

								public virtual Uczen Uczen { get; set; }
								public virtual Nauczyciel Nauczyciel { get; set; }
								public virtual Przedmiot Przedmiot { get; set; }
								public virtual Ocena Ocena { get; set; }

								public OcenaHistoria(){}

								public OcenaHistoria(Ocena ocena, int idEdytującego)
								{
												this.ocena = ocena.ocena;
												this.waga = ocena.waga;
												this.data = ocena.data;
												this.tresc = ocena.tresc;
												this.PrzedmiotID = ocena.PrzedmiotID;
												this.NauczycielID = ocena.NauczycielID;
												this.UczenID = ocena.UczenID;
												this.OcenaID = ocena.ID;
												this.IdEdytujacego = idEdytującego;
												this.dataEdycji = DateTime.Now;
								}
				}
}