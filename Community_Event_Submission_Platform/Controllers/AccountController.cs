using Community_Event_Submission_Platform.Models;
using Community_Event_Submission_Platform.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(User.Login request)
        {
            try
            {
                DataTable response = new DataTable();
                response = AccountService.Login(request);

                if (response != null && response.Rows.Count > 0)
                {
                    Session["id"] = response.Rows[0]["id"].ToString();
                    Session["username"] = response.Rows[0]["username"].ToString();
                    return Json(new
                    {
                        success = true,
                        message = "Login successful!",
                        redirectUrl = Url.Action("Home", "Client")
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid username or password."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Login error: " + ex.Message
                });
            }
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(User.Register request)
        {
            try
            {
                DataTable response = AccountService.Registration(request);

                if (response != null && response.Rows.Count > 0)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Registration successful!",
                        redirectUrl = Url.Action("Login", "Account")
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Registration failed. Username may already exist."
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Registration error: " + ex.Message
                });
            }
        }
    }
}