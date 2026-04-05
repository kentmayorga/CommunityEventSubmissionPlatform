using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Models
{
    public class Events
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string event_date { get; set; }
        public string event_time { get; set; }
        public string location { get; set; }
        public HttpPostedFileBase image_url { get; set; }
        public string image_path { get; set; }
    }
}