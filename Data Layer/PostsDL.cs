using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Web_API.Models;

namespace Web_API.Data_Layer
{
    public class PostsDL
    {
        string _connectionString = "";
        public PostsDL() 
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BlogDBCN"].ConnectionString;   
        }

        public DataTable GetAllPosts()
        {
            try
            {
                DataTable postTable = new DataTable();
                using(SqlConnection con = new SqlConnection( _connectionString))
                {
                    SqlCommand command = new SqlCommand("AllPosts", con);
                    command.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(postTable);
                    con.Close();
                }
                return postTable;
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
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("CreatePost", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", post.userID);
                    command.Parameters.AddWithValue("@userName", post.userName);
                    command.Parameters.AddWithValue("@category", post.category);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Body", post.Body);


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
                   + " is encountered in AddPost due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string UpdatePost(Post post)
        {
            try
            {
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("UpdatePost", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@postID", post.postId);
                    command.Parameters.AddWithValue("@category", post.category);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Body", post.Body);


                    con.Open();
                    command.ExecuteNonQuery();

                    response = "";
                    con.Close();
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in UpdatePost in DL due to "
                   + exception.Message, exception.InnerException);
            }
        }

        public string DeletePost(int postID)
        {
            try
            {
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("DeletePost", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@postID", postID);


                    con.Open();
                    command.ExecuteNonQuery();
                    response = "";
                    con.Close();
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception("An exception of type " + exception.GetType().ToString()
                   + " is encountered in DeletePost in DL due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}