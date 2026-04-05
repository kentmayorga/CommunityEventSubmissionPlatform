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

        public static DataTable CreateEvent(Events response)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    { "@title", response.title },
                    { "@description", response.description },
                    { "@category", response.category },
                    { "@event_date", response.event_date },
                    { "@event_time", response.event_time },
                    { "@location", response.location },
                    { "@image_url", response.image_path ?? "" } 
                };

                var query = @"INSERT INTO event_submission 
                              (title, description, category, location, image_url, event_date, event_time) 
                              VALUES (@title, @description, @category, @location, @image_url, @event_date, @event_time, 'published')";
                SqlRequest.ExecuteQuery(query, parameters);
                return GetEventByUserId(Convert.ToInt32(response.id));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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