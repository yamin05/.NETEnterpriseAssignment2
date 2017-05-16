using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Assignment2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeDB();
        }

        private void InitializeDB()
        {
            var identity = new ApplicationDbContext();
            if (!identity.Database.Exists())
            {
                await Task.Run() => identity.Database.Initialize(true);
            }
            var projectdb = new ProjectDBContext();
            if (!projectdb.Database.Exists())
            {
                projectdb.Database.Initialize(true);
            }
        }
    }
}
