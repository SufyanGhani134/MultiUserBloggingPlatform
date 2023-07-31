using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_API.Models
{
    public class Post
    {
        public int postId { get; set; }

        public int userID { get; set; }

        public string userName { get; set; }
        public string category { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public DateTime dateTime { get; set; }

  

    }
}