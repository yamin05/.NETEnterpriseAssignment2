using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;
using System.Web;

namespace Assignment2.Helpers
{
    public class UsersHelper
    {
        private UsersDao usersDao;
        public UsersHelper()
        {
            usersDao = new UsersDao();
        }

        public IList<UserViewModel> ListOfUsers()
        {
            IList<UserViewModel> viewList = new List<UserViewModel>();
            viewList = usersDao.GetUsersView();
            //foreach (var u in viewList)
            //{
            //    u.District = Enum.GetName((typeof(Districts)), u.District);
            //}
            return viewList;
        }
    }
}