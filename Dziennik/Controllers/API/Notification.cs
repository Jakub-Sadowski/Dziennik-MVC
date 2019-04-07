using Dziennik.Models;
using System;

namespace Dziennik.Controllers.API
{
				public class Notification
				{
								public string Message { get; set; }
								public string Title { get; set; }
								public int ID { get; set; }

								public Notification(Ogloszenie ogloszenie)
								{
												Message = ogloszenie.tresc;
												Title = ogloszenie.naglowek;
												ID = ogloszenie.ID;
								}

								public Notification()
								{
								}
				}
}