using Community_Event_Submission_Platform.Service;
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
            try
            {
                
                ViewBag.TotalUsers = AdminDashboardService.GetTotalUsers();
                ViewBag.TotalEvents = AdminDashboardService.GetTotalEvents();
                ViewBag.UserGrowthPct = AdminDashboardService.GetUserGrowthPercent();

                
                ViewBag.ChartLabels = AdminDashboardService.GetChartLabels();
                ViewBag.ChartUsersData = AdminDashboardService.GetMonthlyUserCounts();
                ViewBag.ChartEventsData = AdminDashboardService.GetMonthlyEventCounts();

                
                DataTable response = AdminDashboardService.GetAllEvents();

                if (response != null && response.Rows.Count > 0)
                {
                    ViewBag.RecentEvents = response;
                }
                else
                {
                    ViewBag.RecentEvents = null;
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dashboard error: " + ex.Message;
                return View();
            }
        }
        public ActionResult Event()
        {
            ViewBag.EventList = Service.EventsService.GetAllEvents();

            return View();
        }
    }
}