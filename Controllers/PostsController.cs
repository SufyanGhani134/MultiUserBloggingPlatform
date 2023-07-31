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
    public class PostsController : ApiController
    {
        public PostsBL BusinessLayer = new PostsBL();

        [HttpGet]
        [Route("GetAllPosts")]

        public List<Post> GetListofAllPosts()
        {
            try
            {
                List<Post> posts = new List<Post>();
                posts = BusinessLayer.GetAllPosts();
                return posts;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfAllPosts due to "
                   + exception.Message, exception.InnerException);
            }

        }
        
        
        [HttpPost]
        [Route("AddPost")]
        public string AddPost([FromBody] Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.AddPost(post);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "Post Added Successfully!";
                    }
                    else
                    {
                        return "Post is not Added!";
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
                   + " is encountered in AddPost due to "
                   + exception.Message, exception.InnerException);
            }
        }

        [HttpPut]
        [Route("UpdatePost")]
        public string UpdatePost([FromBody] Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.UpdatePost(post);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "Post Added Successfully!";
                    }
                    else
                    {
                        return "Post is not Added!";
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
                   + " is encountered in UpdatePost in Controller due to "
                   + exception.Message, exception.InnerException);
            }
        }

        [HttpDelete]
        [Route("DeletePost")]
        public string DeletePost(int postID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.DeletePost(postID);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "Post Added Successfully!";
                    }
                    else
                    {
                        return "Post is not Added!";
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
                   + " is encountered in DeletePost in Controller due to "
                   + exception.Message, exception.InnerException);
            }
        }

    }
}
