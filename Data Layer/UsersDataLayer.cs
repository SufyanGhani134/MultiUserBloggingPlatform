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
    public class UsersDL
    {
            string _connectionString = "";
            public UsersDL()
            {
                _connectionString = WebConfigurationManager.ConnectionStrings["BlogDBCN"].ConnectionString;
            }

            public DataTable GetAllUsers()
            {
                try
                {
                    //List<User> users = new List<User>();
                    DataTable usersTable = new DataTable();
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("AllUsers", con);
                        command.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(usersTable);
                        con.Close();
                    }
                    return usersTable;
                }
                catch (Exception exception)
                {
                    throw new Exception("An exception of type " + exception.GetType().ToString()
                       + " is encountered in GetAllUsers due to "
                       + exception.Message, exception.InnerException);
                }

            }

            public string InsertUser(User user)
            {
                try
                {
                    string response = "";
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        SqlCommand command = new SqlCommand("SignUp", con);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@userName", user.userName);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@password", user.password);

                        command.Parameters.Add("@PKID", SqlDbType.Int, 32);
                        command.Parameters["@PKID"].Direction = ParameterDirection.Output;

                        con.Open();
                        command.ExecuteNonQuery();

                        response = Convert.ToString(command.Parameters["@PKID"].Value);
                        con.Close();
                    }
                    return response;
                }
                catch (Exception exception)
                {
                    throw new Exception("An exception of type " + exception.GetType().ToString()
                       + " is encountered in InsertUserInDB due to "
                       + exception.Message, exception.InnerException);
                }
            }
    }
}