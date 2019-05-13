using Dziennik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.ViewModels
{
	public class WychowankowieVM
	{
		public List<Klasa> Klasy { get; set; }
		public List<Uczen> Uczniowie { get; set; }
		public string SelectedClassName { get; set; }

		public WychowankowieVM(List<Klasa> klasy, List<Uczen> uczniowie, string selectedClassName)
		{
			Klasy = klasy;
			Uczniowie = uczniowie;
			SelectedClassName = selectedClassName == null ? "" : selectedClassName;
		}
	}
}