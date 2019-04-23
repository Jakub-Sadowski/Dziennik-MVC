using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
				public enum MultimediaType
				{
								Sound = 1,
								Picture = 2,
								Video = 3,
				}
				public class Multimedia
				{
								public int ID { get; set; }
								[DisplayName("Multimedia")]public string Path { get; set; }
								[DisplayName("Typ")] public MultimediaType Type { get; set; }
				}
}