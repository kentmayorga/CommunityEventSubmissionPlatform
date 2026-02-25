using Community_Event_Submission_Platform.Models;
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

        public static DataTable GetEventByUserId(int id)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    { "@user_id", id.ToString() }
                };
                var query = @"Select * from event_submission where submitter_id = @user_id";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}