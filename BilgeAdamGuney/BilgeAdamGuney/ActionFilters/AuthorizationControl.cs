using BilgeAdamGuney.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BilgeAdamGuney.ActionFilters
{
    public class AuthorizationControl : ActionFilterAttribute
    {
        public int RoleId { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Member member = (Member)HttpContext.Current.Session["Member"];
            if (member==null||member.RoleId < RoleId)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
            }
            else
                base.OnActionExecuting(filterContext);
        }

    }
}