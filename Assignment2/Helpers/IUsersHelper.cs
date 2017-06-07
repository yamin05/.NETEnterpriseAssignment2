using System.Collections.Generic;
using Assignment2.Models;

namespace Assignment2.Helpers
{
    public interface IUsersHelper
    {
        List<string> GetDistrictList();
        UserViewModel GetUserData(string userId);
        IList<UserViewModel> ListOfUsers();
        void UpdateDistrictForUser(string userId, string district);
    }
}