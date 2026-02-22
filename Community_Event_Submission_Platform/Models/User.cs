using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Models
{
    public class User
    {
        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; } 
        }
        public class Register 
        {
            public string id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string email { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string address { get; set; }
        }
        public class ResetPassword
        {
            public string Email { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}