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
    public class CategoryController : ApiController
    {
        public CategoryBL BusinessLayer = new CategoryBL();

        [HttpGet]
        [Route("GetAllCategories")]

        public List<Categories> GetListofAllCategories()
        {
            try
            {
                List<Categories> categories = new List<Categories>();
                categories = BusinessLayer.GetAllCategories();
                return categories;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetListOfAllCategories due to "
                   + exception.Message, exception.InnerException);
            }

        }

        [HttpPost]
        [Route("AddCategory")]
        public string AddCategory([FromBody] Categories category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resposne = BusinessLayer.AddCategory(category);
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
                   + " is encountered in AddCategory due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}
