using Assignment2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Assignment2.Data_Access_Layer
{
    public class IdentityDBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>

    //TODO: Change the inherited class to IfModelChange before submition

    {
        protected override void Seed(ApplicationDbContext context)
        {
            CreateIdentityUsers(context).Wait();
        }

        private async Task CreateIdentityUsers(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>();
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists(Roles.ADMIN))
            {
                var role = new IdentityRole();
                role.Name = Roles.ADMIN;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(Roles.MANAGER))
            {
                var role = new IdentityRole();
                role.Name = Roles.MANAGER;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(Roles.SITE_ENGINEER))
            {
                var role = new IdentityRole();
                role.Name = Roles.SITE_ENGINEER;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(Roles.ACCOUNTANT))
            {
                var role = new IdentityRole();
                role.Name = Roles.ACCOUNTANT;
                roleManager.Create(role);
            }

            var manager1 = new ApplicationUser() { UserName = "Manager1", Email = "manager1@enet.com" };
            IdentityResult resultManager1 = await userManager.CreateAsync(manager1, "123456");
            if (resultManager1.Succeeded)
            {
                userManager.AddToRole(manager1.Id, Roles.MANAGER);
            }
            var managerYamin = new ApplicationUser() { UserName = "ManagerYamin", Email = "yams.stj@gmail.com" };
            IdentityResult resultManagerYamin = await userManager.CreateAsync(managerYamin, "123456");
            if (resultManagerYamin.Succeeded)
            {
                userManager.AddToRole(managerYamin.Id, Roles.MANAGER);
            }
            var manager2 = new ApplicationUser() { UserName = "Manager2", Email = "manager2@enet.com" };
            IdentityResult resultManager2 = await userManager.CreateAsync(manager2, "123456");
            if (resultManager2.Succeeded)
            {
                userManager.AddToRole(manager2.Id, Roles.MANAGER);
            }
            var manager3 = new ApplicationUser() { UserName = "Manager3", Email = "manager3@enet.com" };
            IdentityResult resultManager3 = await userManager.CreateAsync(manager3, "123456");
            if (resultManager3.Succeeded)
            {
                userManager.AddToRole(manager3.Id, Roles.MANAGER);
            }
            var manager4 = new ApplicationUser() { UserName = "Manager4", Email = "manager4@enet.com" };
            IdentityResult resultManager4 = await userManager.CreateAsync(manager4, "123456");
            if (resultManager4.Succeeded)
            {
                userManager.AddToRole(manager4.Id, Roles.MANAGER);
            }
            var manager5 = new ApplicationUser() { UserName = "Manager5", Email = "manager5@enet.com" };
            IdentityResult resultManager5 = await userManager.CreateAsync(manager5, "123456");
            if (resultManager5.Succeeded)
            {
                userManager.AddToRole(manager5.Id, Roles.MANAGER);
            }
            var manager6 = new ApplicationUser() { UserName = "Manager6", Email = "manager6@enet.com" };
            IdentityResult resultManager6 = await userManager.CreateAsync(manager6, "123456");
            if (resultManager6.Succeeded)
            {
                userManager.AddToRole(manager6.Id, Roles.MANAGER);
            }
            var accountant = new ApplicationUser() { UserName = "Accountant", Email = "accountant@enet.com" };
            IdentityResult resultAccountant = await userManager.CreateAsync(accountant, "123456");
            if (resultAccountant.Succeeded)
            {
                userManager.AddToRole(accountant.Id, Roles.ACCOUNTANT);
            }
            var siteEngineer1 = new ApplicationUser() { UserName = "siteengineer1", Email = "siteengineer1@enet.com" };
            IdentityResult resultSiteEngineer1 = await userManager.CreateAsync(siteEngineer1, "123456");
            if (resultSiteEngineer1.Succeeded)
            {
                userManager.AddToRole(siteEngineer1.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineerYamin = new ApplicationUser() { UserName = "siteengineerYamin", Email = "yams_stj@hotmail.com" };
            IdentityResult resultSiteEngineerYamin = await userManager.CreateAsync(siteEngineerYamin, "123456");
            if (resultSiteEngineerYamin.Succeeded)
            {
                userManager.AddToRole(siteEngineerYamin.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer2 = new ApplicationUser() { UserName = "siteengineer2", Email = "siteengineer2@enet.com" };
            IdentityResult resultSiteEngineer2 = await userManager.CreateAsync(siteEngineer2, "123456");
            if (resultSiteEngineer2.Succeeded)
            {
                userManager.AddToRole(siteEngineer2.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer3 = new ApplicationUser() { UserName = "siteengineer3", Email = "siteengineer3@enet.com" };
            IdentityResult resultsiteEngineer3 = await userManager.CreateAsync(siteEngineer3, "123456");
            if (resultsiteEngineer3.Succeeded)
            {
                userManager.AddToRole(siteEngineer3.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer4 = new ApplicationUser() { UserName = "siteengineer4", Email = "siteengineer4@enet.com" };
            IdentityResult resultsiteEngineer4 = await userManager.CreateAsync(siteEngineer4, "123456");
            if (resultsiteEngineer4.Succeeded)
            {
                userManager.AddToRole(siteEngineer4.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer5 = new ApplicationUser() { UserName = "Siteengineer5", Email = "siteengineer5@enet.com" };
            IdentityResult resultsiteEngineer5 = await userManager.CreateAsync(siteEngineer5, "123456");
            if (resultsiteEngineer5.Succeeded)
            {
                userManager.AddToRole(siteEngineer5.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer6 = new ApplicationUser() { UserName = "Siteengineer6", Email = "siteengineer6@enet.com" };
            IdentityResult resultsiteEngineer6 = await userManager.CreateAsync(siteEngineer6, "123456");
            if (resultsiteEngineer6.Succeeded)
            {
                userManager.AddToRole(siteEngineer6.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer7 = new ApplicationUser() { UserName = "siteengineer7", Email = "siteengineer7@enet.com" };
            IdentityResult resultsiteEngineer7 = await userManager.CreateAsync(siteEngineer7, "123456");
            if (resultsiteEngineer7.Succeeded)
            {
                userManager.AddToRole(siteEngineer7.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer8 = new ApplicationUser() { UserName = "siteengineer8", Email = "siteengineer8@siteengineer.com" };
            IdentityResult resultsiteEngineer8 = await userManager.CreateAsync(siteEngineer8, "123456");
            if (resultsiteEngineer8.Succeeded)
            {
                userManager.AddToRole(siteEngineer8.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer9 = new ApplicationUser() { UserName = "siteengineer9", Email = "siteengineer9@enet.com" };
            IdentityResult resultsiteEngineer9 = await userManager.CreateAsync(siteEngineer9, "123456");
            if (resultsiteEngineer9.Succeeded)
            {
                userManager.AddToRole(siteEngineer9.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer10 = new ApplicationUser() { UserName = "siteEngineer10", Email = "siteengineer10@enet.com" };
            IdentityResult resultsiteEngineer10 = await userManager.CreateAsync(siteEngineer10, "123456");
            if (resultsiteEngineer10.Succeeded)
            {
                userManager.AddToRole(siteEngineer10.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer11 = new ApplicationUser() { UserName = "siteengineer11", Email = "siteengineer11@enet.com" };
            IdentityResult resultsiteEngineer11 = await userManager.CreateAsync(siteEngineer11, "123456");
            if (resultsiteEngineer11.Succeeded)
            {
                userManager.AddToRole(siteEngineer11.Id, Roles.SITE_ENGINEER);
            }
            var siteEngineer12 = new ApplicationUser() { UserName = "siteengineer12", Email = "siteengineer12@enet.com" };
            IdentityResult resultsiteEngineer12 = await userManager.CreateAsync(siteEngineer12, "123456");
            if (resultsiteEngineer12.Succeeded)
            {
                userManager.AddToRole(siteEngineer12.Id, Roles.SITE_ENGINEER);
            }
        }
    }
}