using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Web_API.BusinessLayer;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class UsersController : ApiController
    {
        public UsersBL BusinessLayer = new UsersBL();

        [HttpGet]
        [Route("GetAllUsers")]

        public List<User> GetListofAllUsers() 
        {
            try
            {
                List<User> users = new List<User>();
                users = BusinessLayer.GetAllUsers();
                return users;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfAllUsers due to "
                   + exception.Message, exception.InnerException);
            }
            
        }

        [HttpPost]
        [Route("SignUp")]
        public string InsertUser([FromBody] User user)
        {
            Console.WriteLine("Entering function");
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.InsertUser(user);
                    if (!string.IsNullOrEmpty(resposne))
                    {
                        return "User Signed Up Successfully!";
                    }
                    else
                    {
                        return "User Sign Up is UnSuccessfull!";
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
                   + " is encountered in InsertUser due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}
