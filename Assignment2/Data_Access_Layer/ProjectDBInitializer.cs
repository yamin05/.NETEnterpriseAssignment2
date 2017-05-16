using Assignment2.Models;
using Assignment2.Models.Database_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Assignment2.Data_Access_Layer
{
    public class ProjectDBInitializer : DropCreateDatabaseAlways<ProjectDBContext>

    //TODO: Change the inherited class to IfModelChange before submition

    {
        protected override void Seed(ProjectDBContext context)
        {
            CreateUser(context);
        }

        private async void CreateUser(ProjectDBContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            User user;
            IList<User> users = new List<User>();

            var findAccountant = await userManager. FindByEmailAsync("accountant@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findAccountant))
            {

            }
            var findManager = await userManager.FindByEmailAsync("manager1@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager))
            {
                user = new User();
                user.UserId = findManager.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.URBAN_INDONESIA;
                users.Add(user);
            }
            var findSiteEngineer = await userManager.FindByEmailAsync("siteengineer1@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer))
            {
                user = new User();
                user.UserId = findSiteEngineer.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.URBAN_INDONESIA;
                users.Add(user);
            }
            var findSiteEngineer2 = await userManager.FindByEmailAsync("siteengineer2@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer2))
            {
                user = new User();
                user.UserId = findSiteEngineer2.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.URBAN_INDONESIA;
                users.Add(user);
            }
            var findManager2 = await userManager.FindByEmailAsync("manager2@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager2))
            {
                user = new User();
                user.UserId = findManager2.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.RURAL_INDONESIA;
                users.Add(user);
            }
            var findSiteEngineer3 = await userManager.FindByEmailAsync("siteengineer3@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer3))
            {
                user = new User();
                user.UserId = findSiteEngineer3.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.RURAL_INDONESIA;
                users.Add(user);
            }
            var findSiteEngineer4 = await userManager.FindByEmailAsync("siteengineer4@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer4))
            {
                user = new User();
                user.UserId = findSiteEngineer4.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.RURAL_INDONESIA;
                users.Add(user);
            }
            var findManager3 = await userManager.FindByEmailAsync("manager3@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager3))
            {
                user = new User();
                user.UserId = findManager3.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.URBAN_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findSiteEngineer5 = await userManager.FindByEmailAsync("siteengineer5@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer5))
            {
                user = new User();
                user.UserId = findSiteEngineer5.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.URBAN_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findSiteEngineer6 = await userManager.FindByEmailAsync("siteengineer6@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer6))
            {
                user = new User();
                user.UserId = findSiteEngineer6.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.URBAN_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findManager4 = await userManager.FindByEmailAsync("manager4@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager4))
            {
                user = new User();
                user.UserId = findManager4.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.RURAL_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findSiteEngineer7 = await userManager.FindByEmailAsync("siteengineer7@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer7))
            {
                user = new User();
                user.UserId = findSiteEngineer7.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.RURAL_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findSiteEngineer8 = await userManager.FindByEmailAsync("siteengineer8@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer8))
            {
                user = new User();
                user.UserId = findSiteEngineer8.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.RURAL_PAPUA_NEW_GUINEA;
                users.Add(user);
            }
            var findManager5 = await userManager.FindByEmailAsync("manager5@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager5))
            {
                user = new User();
                user.UserId = findManager5.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.SYDNEY;
                users.Add(user);
            }
            var findSiteEngineer9 = await userManager.FindByEmailAsync("siteengineer9@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer9))
            {
                user = new User();
                user.UserId = findSiteEngineer9.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.SYDNEY;
                users.Add(user);
            }
            var findSiteEngineer10 = await userManager.FindByEmailAsync("siteengineer10@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer10))
            {
                user = new User();
                user.UserId = findSiteEngineer10.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.SYDNEY;
                users.Add(user);
            }
            var findManager6 = await userManager.FindByEmailAsync("manager6@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findManager6))
            {
                user = new User();
                user.UserId = findManager6.Id;
                user.MaximumHours = 100;
                user.MaximumCost = 5000;
                user.District = Districts.RURAL_NEW_SOUTH_WALES;
                users.Add(user);
            }
            var findSiteEngineer11 = await userManager.FindByEmailAsync("siteengineer11@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer11))
            {
                user = new User();
                user.UserId = findSiteEngineer11.Id;
                user.MaximumHours = 50;
                user.MaximumCost = 2000;
                user.District = Districts.RURAL_NEW_SOUTH_WALES;
                users.Add(user);
            }
            var findSiteEngineer12 = await userManager.FindByEmailAsync("siteengineer12@enet.com");
            if (!Utils.getInstance.isNullOrEmpty(findSiteEngineer12))
            {
                user = new User();
                user.UserId = findSiteEngineer12.Id;
                user.MaximumHours = 25;
                user.MaximumCost = 1000;
                user.District = Districts.RURAL_NEW_SOUTH_WALES;
                users.Add(user);
            }

            foreach (var Users in users)
            {
                context.Users.Add(Users);
            }
            base.Seed(context);
        }
    }
}