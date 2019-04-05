using Dziennik.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Dziennik
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
												GlobalConfiguration.Configure(WebApiConfig.Register);
												GlobalFilters.Filters.Add(new IdeSieZabicPrzeTenCodebase());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
								}
    }

    public class IdeSieZabicPrzeTenCodebase : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var idCookie = filterContext.HttpContext.Request.Cookies["1"];
            var roleCookie = filterContext.HttpContext.Request.Cookies["5"];
            if (string.IsNullOrEmpty(idCookie?.Value) || string.IsNullOrEmpty(roleCookie?.Value))
                return;
            filterContext.HttpContext.Session["UserID"] =
                CookieEncryption.Decrypt(int.Parse(filterContext.HttpContext.Request.Cookies["1"].Value)).ToString();
            filterContext.HttpContext.Session["UserName"] = filterContext.HttpContext.Request.Cookies["2"].Value;
            filterContext.HttpContext.Session["Name"] = filterContext.HttpContext.Request.Cookies["3"].Value;
            filterContext.HttpContext.Session["Forname"] = filterContext.HttpContext.Request.Cookies["4"].Value;
            filterContext.HttpContext.Session["Status"] = 
                CookieEncryption.Decrypt(filterContext.HttpContext.Request.Cookies["5"].Value);
        }
    }
}
