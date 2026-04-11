using Community_Event_Submission_Platform.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Community_Event_Submission_Platform.Resources.AppSession;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

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

            ViewBag.UserEventList = EventsService.GetEventByUserId(int.Parse(Session["id"].ToString()));

            return View();
        }
        public ActionResult MyProfile()
        {
            if (Session["username"] == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Username = Session["username"].ToString();

            int userId = int.Parse(Session["id"].ToString());

            // Load profile info (bio, location)
            DataTable profile = AccountService.GetProfileById(Session["id"].ToString());
            if (profile != null && profile.Rows.Count > 0)
            {
                ViewBag.Location = profile.Rows[0]["address"]?.ToString();
                ViewBag.Bio = profile.Rows[0]["bio"]?.ToString();
            }

            // Load user events
            DataTable response = EventsService.GetEventByUserId(userId);
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

        [HttpPost]
        public ActionResult UpdateProfile(string username, string address, string bio)
        {
            if (Session["username"] == null)
                return Json(new { success = false, message = "Session expired. Please log in again." });

            try
            {
                string id = Session["id"].ToString();

                // DEBUG: log what we received
                System.Diagnostics.Debug.WriteLine("=== UpdateProfile ===");
                System.Diagnostics.Debug.WriteLine("id: " + id);
                System.Diagnostics.Debug.WriteLine("username: " + username);
                System.Diagnostics.Debug.WriteLine("address: " + address);
                System.Diagnostics.Debug.WriteLine("bio: " + bio);

                DataTable response = AccountService.UpdateProfile(id, username, address, bio);

                System.Diagnostics.Debug.WriteLine("response rows: " + (response?.Rows.Count.ToString() ?? "null"));

                if (response != null && response.Rows.Count > 0)
                {
                    string message = response.Rows[0]["Message"].ToString();
                    string updatedUsername = response.Rows[0]["username"].ToString();

                    Session["username"] = updatedUsername;

                    return Json(new
                    {
                        success = true,
                        message = message,
                        username = updatedUsername,
                        address = response.Rows[0]["address"]?.ToString(),
                        bio = response.Rows[0]["bio"]?.ToString()
                    });
                }
                else
                {
                    return Json(new { success = false, message = "Update failed — response was empty." });
                }
            }
            catch (Exception ex)
            {
                // Return the REAL error so we can see what's wrong
                System.Diagnostics.Debug.WriteLine("UpdateProfile ERROR: " + ex.ToString());
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }



        public ActionResult Logout() 
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }
    }
}