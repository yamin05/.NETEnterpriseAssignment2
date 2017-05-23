using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class ChangeDistrictsHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;
        private UserDetail userDetail = new UserDetail();

        public ChangeDistrictsHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }

        public ChangeDistrictsHelper()
        {

        }

        //Changes districts for users based on userid
        public void ChangeDistricts(string userId, string oldDistrict, int newDistrict)
        {
            userDetail.UserId = userId;
            var userDetailRepo = new UserDetailRepository(context);
            userDetail = userDetailRepo.GetUserWithUserId(userDetail.UserId);
            var oldDistrictText = Convert.ToInt32(oldDistrict);
            var repo = new UserDetailRepository(context);
            var row = repo.ChangeUserDistrict(userDetail.UserId, oldDistrictText, newDistrict);
        }

        //Creates A list of Districts to be used in drop down
        public Dictionary<string, int> GetDistrictForUser()
        {
            Dictionary<string, int> list = new Dictionary<string, int>();      
            list.Add(Districts.Urban_Indonesia.ToString().Replace("_"," "), (int)Districts.Urban_Indonesia);
            list.Add(Districts.Rural_Indonesia.ToString().Replace("_", " "), (int)Districts.Rural_Indonesia);
            list.Add(Districts.Urban_Papua_New_Guinea.ToString().Replace("_", " "), (int)Districts.Urban_Papua_New_Guinea);
            list.Add(Districts.Rural_Papua_New_Guinea.ToString().Replace("_", " "), (int)Districts.Rural_Papua_New_Guinea);
            list.Add(Districts.Sydney.ToString().Replace("_", " "), (int)Districts.Sydney);
            list.Add(Districts.Rural_New_South_Wales.ToString().Replace("_", " "), (int)Districts.Rural_New_South_Wales);
            return list;
        }
    }
}