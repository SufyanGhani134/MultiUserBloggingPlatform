using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static Web_API.Data_Layer.UsersDL;
using Web_API.Models;
using Web_API.Data_Layer;

namespace Web_API.BusinessLayer
{
    public class UsersBL
    {
            public UsersDL userDL = new UsersDL();
            public List<User> GetAllUsers()
            {
                try
                {
                    DataTable table = new DataTable();
                    List<User> users = new List<User>();
                    table = userDL.GetAllUsers();
                    if (table != null && table.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in table.Rows)
                        {
                            User user = new User();
                            user.userId = Convert.ToInt32(dataRow["userID"]);
                            user.userName = dataRow["userName"].ToString();
                            user.email = dataRow["email"].ToString();
                            user.password = dataRow["password"].ToString();
                        users.Add(user);
                        }
                    }
                    return users;
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
                    string response = userDL.InsertUser(user);
                    return response;
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