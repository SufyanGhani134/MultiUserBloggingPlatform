using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Web_API.Data_Layer;
using Web_API.Models;

namespace Web_API.BusinessLayer
{
    public class PostsBL
    {
        public PostsDL postsDL = new PostsDL();
        public List<Post> GetAllPosts()
        {
            try
            {
                DataTable table = new DataTable();
                List<Post> posts = new List<Post>();
                table = postsDL.GetAllPosts();
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        Post post = new Post();
                        post.postId = Convert.ToInt32(dataRow["postID"]);
                        post.userID = Convert.ToInt32(dataRow["userID"]);
                        post.userName = dataRow["userName"].ToString();
                        post.category = dataRow["category"].ToString();
                        post.Title = dataRow["Title"].ToString();
                        post.Body = dataRow["Body"].ToString();
                        post.dateTime = Convert.ToDateTime(dataRow["dateTime"]);
                        posts.Add(post);
                    }
                }
                return posts;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetAllPosts due to "
                   + exception.Message, exception.InnerException);
            }
        }
        public string AddPost(Post post)
        {
            try
            {
                string response = postsDL.AddPost(post);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in AddPost due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string UpdatePost(Post post)
        {
            try
            {
                string response = postsDL.UpdatePost(post);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in UpdatePost in BL due to "
                   + exception.Message, exception.InnerException);
            }
        }
        public string DeletePost(int postID)
        {
            try
            {
                string response = postsDL.DeletePost(postID);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in DeletePost in BL due to "
                   + exception.Message, exception.InnerException);
            }
        }

    }
}