using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Resources
{
    public class AppSession
    {
        public class SessionTimeoutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (HttpContext.Current.Session["id"] == null)
                {
                    filterContext.Result = new RedirectResult("~/");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}