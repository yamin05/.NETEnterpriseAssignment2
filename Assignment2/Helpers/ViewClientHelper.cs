using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class ViewClientHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;

        public ViewClientHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }

        public ViewClientHelper()
        {

        }

        public IList<Clients> GetAllClients ()
        {
            var repos = new ClientRepository(context);
            var userRepo = new UserRepository(context);
            var user = userRepo.GetAllForUser(Utils.getInstance.GetCurrentUserId());
            var list = repos.GetAllClientsForUser(Utils.getInstance.GetCurrentUserId(), user.District);
            return list;
        }

        public IList<Clients> GetAllClientsCreatedByUser()
        {
            var repos = new ClientRepository(context);
            var list = repos.GetAllClientsCreatedByUser(Utils.getInstance.GetCurrentUserId());
            return list;
        }

        public IList<Clients> GetAllClientsInSameDistrict()
        {
            var repos = new ClientRepository(context);
            var list = repos.GetAllClientsInSameDistrict(HttpContext.Current.User.Identity.GetUserId());
            return list;
        }
    }
}