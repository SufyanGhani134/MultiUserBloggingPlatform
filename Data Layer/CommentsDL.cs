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
    public class CommentsDL
    {
        string _connectionString = "";
        public CommentsDL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BlogDBCN"].ConnectionString;
        }

        public DataTable GetAllComments()
        {
            try
            {
                DataTable commentsTable = new DataTable();
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("AllComments", con);
                    command.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = command;
                    dataAdapter.Fill(commentsTable);
                    con.Close();
                }
                return commentsTable;
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
                string response = "";
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand("AddComment", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userID", comment.userID);
                    command.Parameters.AddWithValue("@userName", comment.userName);
                    command.Parameters.AddWithValue("@postID", comment.postID);
                    command.Parameters.AddWithValue("@comment", comment.comment);


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
                   + " is encountered in AddComment due to "
                   + exception.Message, exception.InnerException);
            }
        }
    }
}