using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Event()
        {
            ViewBag.EventList = Service.EventsService.GetAllEvents();

            return View();
        }
    }
}