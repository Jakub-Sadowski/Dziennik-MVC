using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Dziennik.Helpers
{
				public static class EmailHelper
				{
								public static readonly string APP_EMAIL = "mojagracv@gmail.com";
								public static async void Send(string emailTo, string emailFrom, string body, string subject, bool htmlBody = true)
								{
												var message = new MailMessage();

												message.To.Add(new MailAddress(emailTo));

												message.From = new MailAddress(emailFrom);
												message.Subject = subject;
												message.Body = body;
												message.IsBodyHtml = htmlBody;

												using (var smtp = new SmtpClient())
												{
																var credential = new NetworkCredential
																{
																				UserName = "mojagracv@gmail.com",  // replace with valid value
																				Password = "civilization96"  // replace with valid value
																};
																smtp.Credentials = credential;
																smtp.Host = "smtp.gmail.com";
																smtp.Port = 587;
																smtp.EnableSsl = true;
																await smtp.SendMailAsync(message);
												}
								}
				}
}