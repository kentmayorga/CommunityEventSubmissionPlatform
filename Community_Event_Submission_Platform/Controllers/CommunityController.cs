using Community_Event_Submission_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Controllers
{
    public class CommunityController : Controller
    {
        public ActionResult JoinOrCreateCommunityForm()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCommunity(Community model)
        {
            return View();
        }

        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JoinCommunity(Community model)
        {
            return View();
        }
    }
}