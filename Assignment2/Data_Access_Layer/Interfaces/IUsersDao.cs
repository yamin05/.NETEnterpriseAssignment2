using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Data_Access_Layer
{
    public interface IUsersDao
    {
        IList<UserViewModel> GetUsersView();
        UserViewModel GetUserData(string userID);
        void UpdateDistrictForUser(string userId, string district);
    }
}