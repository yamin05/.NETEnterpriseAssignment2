﻿using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;
using System.Web;

namespace Assignment2.Helpers
{
    public  class UsersHelper : IUsersHelper
    {
        private IUsersDao usersDao;
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

        /// <summary>
        /// This method is for update district for user with user id and district
        /// </summary>
        public void UpdateDistrictForUser(string userId, string district)

        {
            try
            {
                var Users = new UsersHelper();
                UserViewModel userDetail = Users.GetUserData(userId);
                if (userDetail.District.Equals(district))
                {
                    throw new CannotEditDistrictException();
                }

                usersDao.UpdateDistrictForUser(userId, district);
            }
            catch(Exception ex)
            {
                if (ex is CannotEditDistrictException)
                {
                    throw;
                }
                else
                {
                    throw new FailedToUpdateRecordException();
                }
            }
        }

    }
}