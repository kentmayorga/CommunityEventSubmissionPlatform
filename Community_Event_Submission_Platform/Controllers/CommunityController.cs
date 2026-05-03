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

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            DataTable dt = EventsService.GetAllCommunities();
            return View(dt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Community model)
        {
            //if (Session["user_id"] == null)
            //    return RedirectToAction("Login", "Account");

            try
            {
                DataTable response = EventsService.CreateCommunity(model);
                if (response != null && response.Rows.Count > 0)
                {
                    TempData["Success"] = "Community created successfully!";
                    // ✅ Redirect to Admin Dashboard after successful creation
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    ViewBag.Message = "Failed to create community. Please try again.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }

            return View(model);

        }
        public ActionResult CreateCommunity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCommunity(Community model)
        {
            //try
            //{
            //    DataTable response = new DataTable();
            //    response = EventsService.CreateCommunity(model);

            //    if (response != null && response.Rows.Count > 0)
            //    {

            //        TempData["Success"] = "Community notice posted successfully!";
            //        return RedirectToAction("Create", "Community");
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Failed to create community. Please try again.";
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Message = "Error creating community: " + ex.Message;
            //}
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