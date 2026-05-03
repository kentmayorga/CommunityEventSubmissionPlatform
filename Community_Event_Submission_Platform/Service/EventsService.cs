using Community_Event_Submission_Platform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    { "@user_id", response.id.ToString() },
                    { "@image_url", response.image_path ?? "" }
                };

                var query = @"INSERT INTO event_submission 
                             (title, description, category, location, image_url, event_date, event_time, status, submitter_id) 
                             VALUES (@title, @description, @category, @location, @image_url, @event_date, @event_time, 'published', @user_id)";
                SqlRequest.ExecuteQuery(query, parameters);
                return GetEventByUserId(Convert.ToInt32(response.id));
            }
            catch (Exception)
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

        public static DataTable CreateCommunity(Community response)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    { "@name",         response.name },
                    { "@description",  response.description },
                    { "@privacy",      response.privacy ?? "public" },
                    { "@created_by",   response.created_by.ToString() },
                    { "@created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                    { "@modified_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
                };

                string query = @"INSERT INTO communities 
                            (name, description, privacy, created_by, created_date, modified_date) 
                         VALUES 
                            (@name, @description, @privacy, @created_by, @created_date, @modified_date)";

                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception) { throw; }
        }
        public static DataTable GetAllCommunities()
        {
            try
            {
                parameters.Clear();
                string query = @"SELECT * FROM communities 
                         WHERE deleted_date IS NULL 
                         ORDER BY created_date DESC";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception) { throw; }

        }
    }
}