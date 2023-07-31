using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Data_Layer;
using Web_API.Models;

namespace Web_API.BusinessLayer
{
    public class CategoryBL
    {
        public CategoryDL CategoryDL = new CategoryDL();
        public List<Categories> GetAllCategories()
        {
            try
            {
                DataTable table = new DataTable();
                List<Categories> categories = new List<Categories>();
                table = CategoryDL.GetAllCategories();
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in table.Rows)
                    {
                        Categories category = new Categories();
                        category.category = dataRow["category"].ToString();
                        categories.Add(category);
                    }
                }
                return categories;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in GetAllCategories due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string AddCategory(Categories category)
        {
            try
            {
                string response = CategoryDL.AddCategory(category);
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in AddCategoy due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}