using System.Web.Http;

class WebApiConfig
{
				public static void Register(HttpConfiguration configuration)
				{
								configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
												new { id = RouteParameter.Optional });
				}
}