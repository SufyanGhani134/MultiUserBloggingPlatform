using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public int userID { get; set; }

        public string userName { get; set; }
        public int postID { get; set; }
        public string comment { get; set; }
        public DateTime dateTime { get; set; }

        public int getUserID() { return userID; }

        public void setUserID(int value) { userID = value; }

    }
}