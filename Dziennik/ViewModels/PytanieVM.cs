using Dziennik.Helpers;
using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;

namespace Dziennik.ViewModels
{
				public class PytanieVM
				{
								public int ID { get; set; }
								public int? TestID { get; set; }
								[DisplayName("Treść pytania")] public string tresc { get; set; }
								public string odpowiedz1 { get; set; }
								public string odpowiedz2 { get; set; }
								public string odpowiedz3 { get; set; }
								public string odpowiedz4 { get; set; }
								public int punktacja { get; set; }
								public odp? odp { get; set; }


								public List<Multimedia> Sounds { get; set; }
								public List<Multimedia> Pictures { get; set; }
								public List<Multimedia> Videos { get; set; }

								public PytanieVM(Pytanie pytanie)
								{
												ID = pytanie.ID;
												TestID = pytanie.TestID;
												this.tresc = pytanie.tresc;
												this.odpowiedz1 = pytanie.odpowiedz1;
												this.odpowiedz2 = pytanie.odpowiedz2;
												this.odpowiedz3 = pytanie.odpowiedz3;
												this.odpowiedz4 = pytanie.odpowiedz4;
												this.punktacja = pytanie.punktacja;
												this.odp = pytanie.odp;
												Sounds = pytanie.Multimedia.Where( x => x.Type == MultimediaType.Dźwięk).ToList();
												Pictures = pytanie.Multimedia.Where(x => x.Type == MultimediaType.Obrazek).ToList();
												Videos = pytanie.Multimedia.Where(x => x.Type == MultimediaType.Wideo).ToList();
								}

								/// <summary>
								/// usuwa przedrosetk guid kiedy chcemy tylko pokazywać użytkownikowi czytelną nazwe i nie potrzebna jest 'prawdziwa' nazwa pliku
								/// </summary>
								public void RemoveGuid()
								{
												foreach (var m in Sounds)
																m.Path = FileHandler.GetFileName(m.Path);
												foreach (var m in Pictures)
																m.Path = FileHandler.GetFileName(m.Path);
												foreach (var m in Videos)
																m.Path = FileHandler.GetFileName(m.Path);
								}

								public void SetPathsToBase64()
								{
												foreach (var m in Sounds)
																m.Path = FileHandler.GetBase64(m.Path);
												foreach (var m in Pictures)
																m.Path = FileHandler.GetBase64(m.Path);
												foreach (var m in Videos)
																m.Path = FileHandler.GetBase64(m.Path);
								}
				}
}