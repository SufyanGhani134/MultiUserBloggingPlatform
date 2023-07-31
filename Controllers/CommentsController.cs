using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.BusinessLayer;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class CommentsController : ApiController
    {
        public CommentsBL BusinessLayer = new CommentsBL();

        [HttpGet]
        [Route("GetAllComments")]

        public List<Comment> GetListofAllCommentss()
        {
            try
            {
                List<Comment> comments = new List<Comment>();
                comments = BusinessLayer.GetAllComments();
                return comments;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfAllComments due to "
                   + exception.Message, exception.InnerException);
            }

        }

        [HttpPost]
        [Route("AddComment")]
        public string AddComment([FromBody] Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.AddComment(comment);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "Comment Added Successfully!";
                    }
                    else
                    {
                        return "Comment is not Added!";
                    }
                }
                else
                {
                    return "Model Is Not Valid";
                }
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
