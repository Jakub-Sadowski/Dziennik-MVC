using System.Web.Mvc;
using System.Web.Routing;

namespace Dziennik.ActionAttrs
{
    public class RedirectIfNotAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            if (filterContext.HttpContext.Session["Status"] != "Admin")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}