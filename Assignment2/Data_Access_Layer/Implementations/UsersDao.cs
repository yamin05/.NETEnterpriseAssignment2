using System;
using System.Linq;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using WebApplication2.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using Assignment2.Data_Access_Layer;
using System.Collections;
using Microsoft.AspNet.Identity;

namespace Assignment2.Data_Access_Layer
{
    public class UsersDao : IUsersDao
    {
        private CustomDBContext context;
        private ApplicationDbContext context1;

        public IList<UserViewModel> GetUsersView()
        {
            context = new CustomDBContext();
            context1 = new ApplicationDbContext();
            {
                var userList = (from tb1 in context1.Users
                                from tb2 in tb1.Roles
                                join tb3 in context1.Roles on tb2.RoleId equals tb3.Id
                                where tb3.Name == "Site_Engineer" || tb3.Name == "Manager"
                                select new { tb1.Id, Name = tb3.Name.Replace("_", " "), tb1.UserName }).ToList();

                var userDetail = (from tb1 in context.Users
                                  select tb1).ToList();

                IList<UserViewModel> userView = (from tb1 in userList
                                                 join tb2 in userDetail
                                                 on tb1.Id equals tb2.UserId
                                                 orderby tb1.UserName
                                                 select new UserViewModel()
                                                 {
                                                     UserId = tb2.UserId,
                                                     RoleName = tb1.Name,
                                                     UserName = tb1.UserName,
                                                     MaximumCost = tb2.MaximumCost,
                                                     MaximumHours = tb2.MaximumHours,
                                                     District = tb2.District
                                                 }).ToList();
                return userView;
            }
        }


        public UserViewModel GetUserData(string userID)
        {
            IList<UserViewModel> userList = GetUsersView();
            {
                UserViewModel userData = (from tb1 in userList
                                          where tb1.UserId == userID
                                          select new UserViewModel()
                                          {
                                              UserId = tb1.UserId,
                                              RoleName = tb1.RoleName,
                                              UserName = tb1.UserName,
                                              MaximumCost = tb1.MaximumCost,
                                              MaximumHours = tb1.MaximumHours,
                                              District = tb1.District
                                          }).FirstOrDefault();
                return userData;
            }
        }

        public void UpdateDistrictForUser(string userId, string district)
        {
            context = new CustomDBContext();
            {
                User user = context.Users.Single(u => u.UserId == userId);
                user.District = district;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }
        }
    }
}