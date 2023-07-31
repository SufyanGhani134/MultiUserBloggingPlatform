using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Data_Layer;
using Web_API.Models;

namespace Web_API.BusinessLayer
{
    public class CommentsBL
    {
        public CommentsDL commentsDL = new CommentsDL();
        public List<Comment> GetAllComments()
        {
            try
            {
                DataTable table = new DataTable();
                List<Comment> comments = new List<Comment>();
                table = commentsDL.GetAllComments();
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        Comment comment = new Comment();
                        comment.commentId = Convert.ToInt32(dataRow["commentID"]);
                        comment.setUserID(Convert.ToInt32(dataRow["userID"]));
                        comment.userName = dataRow["userName"].ToString();
                        comment.postID = Convert.ToInt32(dataRow["postID"]);
                        comment.comment = dataRow["comment"].ToString();
                        comment.dateTime = Convert.ToDateTime(dataRow["dateTime"]);
                        comments.Add(comment);
                    }
                }
                return comments;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetAllComments due to "
                   + exception.Message, exception.InnerException);
            }
        }
        public string AddComment(Comment comment)
        {
            try
            {
                string response = commentsDL.AddComment(comment);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in AddComment due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}