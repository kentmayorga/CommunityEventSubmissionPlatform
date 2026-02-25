using Community_Event_Submission_Platform.Service;
using System;
using System.Collections.Generic;
using System.Data;
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
            ViewBag.EventList = EventsService.GetAllEvents();

            return View();
        }

        public ActionResult MyEvent()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Account");
            ViewBag.Username = Session["username"].ToString();
         
            int userId = int.Parse(Session["id"].ToString());
           
            DataTable response = new DataTable();
            response = EventsService.GetEventByUserId(userId);

            if (response != null && response.Rows.Count > 0)
            {
                ViewBag.events = response;
            }
            else 
            {
                ViewBag.Message = "No events yet.";
            }

            return View();
        }
        public ActionResult MyProfile()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = Session["username"].ToString();

            int userId = int.Parse(Session["id"].ToString());

            DataTable response = new DataTable();
            response = EventsService.GetEventByUserId(userId);

            if (response != null && response.Rows.Count > 0)
            {
                ViewBag.events = response;
            }
            else
            {
                ViewBag.Message = "No events yet.";
            }

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