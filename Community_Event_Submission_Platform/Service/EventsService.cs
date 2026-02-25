using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Service
{
    public class EventsService
    {
        public static Dictionary<string, string> parameters = new Dictionary<string, string>();

        public static DataTable GetAllEvents() 
        {
            try
            {
                string query = "SELECT * FROM event_submission WHERE status IS NOT NULL";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}