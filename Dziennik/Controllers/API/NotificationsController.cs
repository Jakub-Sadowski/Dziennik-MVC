using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dziennik.Controllers.API
{
				public class NotificationsController : ApiController
				{
								private static ConcurrentBag<StreamWriter> clients;
								private static HashSet<int?> clientsID;
								static NotificationsController()
								{
												clients = new ConcurrentBag<StreamWriter>();
												clientsID = new HashSet<int?>();
								}

								public async Task PostNotificationAsync(Notification notification)
								{
												foreach (var client in clients)
												{
																try
																{
																				var data = $"data:{JsonConvert.SerializeObject(notification, Formatting.None)}\n\n";
																				await client.WriteAsync(data);
																				await client.FlushAsync();
																				client.Dispose();
																}
																catch (Exception)
																{
																				StreamWriter ignore;
																				clients.TryTake(out ignore);
																}
												}
								}

								[HttpGet]
								public HttpResponseMessage Subscribe(HttpRequestMessage request)
								{
												var response = request.CreateResponse();
												response.Content = new PushStreamContent((a, b, c) =>
												{ OnStreamAvailable(a, b, c); }, "text/event-stream");
												return response;
								}

								private void OnStreamAvailable(Stream stream, HttpContent content,
												TransportContext context)
								{
												var client = new StreamWriter(stream);
												clients.Add(client);
								}
				}
}