using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Community_Event_Submission_Platform.Resources.AppSession;

namespace Community_Event_Submission_Platform.Controllers
{
    public class ClientController : Controller
    {
        [SessionTimeout]
        public ActionResult Home() 
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = Session["username"].ToString();

            return View();
        }

        public ActionResult MyEvent()
        {
            return View();
        }

        public ActionResult Logout() 
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }
    }
}