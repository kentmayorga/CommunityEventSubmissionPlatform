using Community_Event_Submission_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login() 
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(User.Login request) 
        {
            return View();
        }

        public ActionResult Register() 
        {
            return View();
        }   
        [HttpPost]
        public ActionResult Register(User.Register request)
        {
            return View();
        }
    }
}