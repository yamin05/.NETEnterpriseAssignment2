using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Data_Access_Layer
{
    public class UserDao : IUserDao
    {
        private CustomDBContext context;

        /// <summary>
        /// This method is used for getting current users data
        /// </summary>
        /// <param name="userID">Id of the current User</param>
        /// <returns>user</returns>
        public User GetUser(string userID)
        {
            using (context = new CustomDBContext())
            {
                var user = context.Users
                           .Where(u => u.UserId.Equals(userID))
                           .Select(u => u)
                           .FirstOrDefault();
                return user;
            }
        }

        /// <summary>
        /// This method is used for getting current users district with user id
        /// </summary>
        /// <returns>district</returns>
        public string GetUserDistrict(string userId)
        {
            using (context = new CustomDBContext())
            {
                var district = context.Users
                                .Where(u => u.UserId.Equals(userId))
                                .Select(u => u.District)
                                .FirstOrDefault();
                return district;
            }
        }
    }
}