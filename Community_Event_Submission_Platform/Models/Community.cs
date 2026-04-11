using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Event_Submission_Platform.Models
{
    public class Community
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string invite_code { get; set; }
        public int created_by { get; set; }
        public string created_date { get; set; }
    }

    public class CommunityMember
    {
        public int id { get; set; }
        public int community_id { get; set; }
        public int user_id { get; set; }
        public string role { get; set; } // "admin" or "member"
        public string joined_date { get; set; }
    }

    public class JoinCommunity
    {
        public string invite_code { get; set; }
    }
}