using Community_Event_Submission_Platform.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

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

        public static DataTable ForgotPassword(User.Login Forgot)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
        {
            { "@username", Forgot.username },
            { "@email", Forgot.email }  // lowercase
        };

                var query = @"
                            SELECT
                                CASE WHEN EXISTS (SELECT 1 FROM users WHERE username = @username AND email = @email AND deleted_date IS NULL) 
                                     THEN 'User found.' 
                                ELSE 'Username or email not found.' 
                                END AS Message,
                                CASE WHEN EXISTS (SELECT 1 FROM users WHERE username = @username AND email = @email AND deleted_date IS NULL) 
                                     THEN 1 ELSE 0 
                                END AS Success,
                                email FROM users WHERE username = @username AND email = @email AND deleted_date IS NULL
                        ";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable ResetPassword(User.ResetPassword request)
        {
            try
            {
                parameters.Clear();
                parameters = new Dictionary<string, string>
        {
            { "@email", request.Email },
            { "@newpassword", request.NewPassword }
        };

                var query = @"
                            UPDATE users 
                            SET password = @newpassword 
                            WHERE email = @email AND deleted_date IS NULL;

                            SELECT
                                CASE WHEN ROW_COUNT() > 0 
                                     THEN 'Password reset successful.' 
                                ELSE 'Email not found.' 
                                END AS Message;
                        ";
                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}