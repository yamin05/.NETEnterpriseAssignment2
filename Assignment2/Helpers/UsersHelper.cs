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
            return viewList;
        }

        public UserViewModel GetUserData(string userId)
        {
            UserViewModel userDetail = usersDao.GetUserData(userId);
            return userDetail;
        }

        public List<string> GetDistrictList()
        {
            List<string> districtList = new List<string>();

            districtList.Add(Districts.URBAN_INDONESIA);
            districtList.Add(Districts.RURAL_INDONESIA);
            districtList.Add(Districts.URBAN_PAPUA_NEW_GUINEA);
            districtList.Add(Districts.RURAL_PAPUA_NEW_GUINEA);
            districtList.Add(Districts.SYDNEY);
            districtList.Add(Districts.RURAL_NEW_SOUTH_WALES);
            return districtList;
        }

        public void UpdateDistrictForUser(string userId, string district)
        {
            if (userId!="" && district!="")
            {
                usersDao.UpdateDistrictForUser(userId, district);
            }
        }

    }
}