using System.Collections.Generic;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class ViewUsersHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;

        public ViewUsersHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }

        public ViewUsersHelper()
        {

        }

        public IList<UserDetail> GetAllUsers()
        {
            var repos = new UserDetailRepository(context);
            var list = repos.GetAllUsers();
            return list;
        }
    }
}