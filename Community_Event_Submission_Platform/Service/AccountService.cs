using Community_Event_Submission_Platform.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Service
{
    public class AccountService
    {
        public static Dictionary<string, string> parameters = new Dictionary<string, string>();

        public static DataTable Login(User.Login request)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    {"@username", request.username},
                    { "@password", request.password}
                };

                var query = "SELECT * FROM users WHERE username = @username AND password = @password AND deleted_date IS NULL";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable Registration(User.Register request)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    { "@username", request.username },
                    { "@password", request.password },
                    { "@email", request.email },
                    { "@firstname", request.firstname },
                    { "@lastname", request.lastname },
                    { "@address", request.address }
                };
                var query = @"
                                insert into users (username, password, email, firstname, lastname, address, role, created_date) 
                                values (@username, @password, @email, @firstname, @lastname, @address, 'user', now())
                            ";
                SqlRequest.ExecuteQuery(query, parameters);
                return GetUserByUsername(request.username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetUserByUsername(string username)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
                {
                    { "@username", username }
                };
                var query = "select * from users where username = @username and deleted_date IS NULL";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}