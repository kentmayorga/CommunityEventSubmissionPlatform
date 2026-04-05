using Community_Event_Submission_Platform.Models;
using Community_Event_Submission_Platform.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Event_Submission_Platform.Controllers
{
    public class EventController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Events model)
        {
            if (model != null)
            {
                if (model.image_url != null && model.image_url.ContentLength > 0)
                {
                    // 1️⃣ Ensure the folder exists
                    string folderPath = Server.MapPath("/Utilities/images/");
                    if (!Directory.Exists(folderPath))
                    {
                       ViewBag.Message = "Image upload failed: 'images' folder does not exist.";
                    }

                    // 2️⃣ Create a unique filename to avoid overwriting
                    string originalFileName = Path.GetFileName(model.image_url.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFileName);

                    // 3️⃣ Save the file
                    string savePath = Path.Combine(folderPath, uniqueFileName);
                    model.image_url.SaveAs(savePath);

                    // 4️⃣ Save the relative URL to database
                    model.image_path = "images/" + uniqueFileName;
                }

                // 5️⃣ Save event to database
                DataTable response = EventsService.CreateEvent(model);

                ViewBag.Message = "Event successfully created!";
            }

            return RedirectToAction("MyEvent","Client");
        }
    }
}