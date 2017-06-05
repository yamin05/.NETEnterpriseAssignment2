using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Data_Access_Layer
{
    public interface IUserDao
    {
        User GetUser(string userID);
        string GetUserDistrict(string userId);
    }
}
