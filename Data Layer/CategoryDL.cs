using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Web_API.Models;

namespace Web_API.Data_Layer
{
    public class CategoryDL
    {
        string _connectionString = "";
        public CategoryDL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BlogDBCN"].ConnectionString;
        }

        public DataTable GetAllCategories()
        {
            try
            {
                DataTable categoryTable = new DataTable();
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("AllCategories", con);
                    command.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(categoryTable);
                    con.Close();
                }
                return categoryTable;
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
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("createCategory", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@category", category.category);

                    con.Open();
                    command.ExecuteNonQuery();

                    response = Convert.ToString(category.category);
                    con.Close();
                }
                return response;
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