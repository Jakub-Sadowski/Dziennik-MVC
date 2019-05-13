using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dziennik.ViewModels
{
	public class MailDoRodzicowKlasyVM
	{
		[Required]public int KlasaId { get; set; }
		[Display(Name = "Treść")] [DataType(DataType.MultilineText)] public string Tresc{ get; set; }

		public MailDoRodzicowKlasyVM(int klasaId, string tresc)
		{
			KlasaId = klasaId;
			Tresc = tresc;
		}

		public MailDoRodzicowKlasyVM()
		{
		}
	}
}