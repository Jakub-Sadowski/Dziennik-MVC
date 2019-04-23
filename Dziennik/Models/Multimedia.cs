using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
				public enum MultimediaType
				{
								Sound = 1,
								Picture = 2,
								Flash = 3,
				}
				public class Multimedia
				{
								public int ID { get; set; }
								public string Path { get; set; }
								public MultimediaType Type { get; set; }
				}
}