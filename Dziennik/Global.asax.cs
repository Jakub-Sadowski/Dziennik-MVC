using Dziennik.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var idCookie = filterContext.HttpContext.Request.Cookies["oWo"];
            var roleCookie = filterContext.HttpContext.Request.Cookies["WHATS_THIS"];
            if (string.IsNullOrEmpty(idCookie?.Value) || string.IsNullOrEmpty(roleCookie?.Value))
                return;
            filterContext.HttpContext.Session["UserID"] =
                SuperDuperCookieSuccurity.Decrypt(int.Parse(filterContext.HttpContext.Request.Cookies["oWo"].Value)).ToString();
            filterContext.HttpContext.Session["UserName"] = filterContext.HttpContext.Request.Cookies["UserName"].Value;
            filterContext.HttpContext.Session["Name"] = filterContext.HttpContext.Request.Cookies["Name"].Value;
            filterContext.HttpContext.Session["Forname"] = filterContext.HttpContext.Request.Cookies["Forname"].Value;
            filterContext.HttpContext.Session["Status"] = 
                SuperDuperCookieSuccurity.Decrypt(filterContext.HttpContext.Request.Cookies["WHATS_THIS"].Value);
        }
    }
}
