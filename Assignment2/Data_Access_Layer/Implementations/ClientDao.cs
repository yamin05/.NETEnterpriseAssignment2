using Assignment2.Models.Database_Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2.Data_Access_Layer
{
    public class ClientDao : IClientDao
    {
        private CustomDBContext context;

        public void AddClient(User user, Client client)
        {
            using (context = new CustomDBContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method is used for getting all clients associated with the user
        /// </summary>
        /// <returns>IList</returns>
        public IList<Client> GetClientsForUser(string userId)
        {
            using (context = new CustomDBContext())
            {
                var clients = context.Clients
                           .Where(c => c.CreatedByUserId.Equals(userId))
                           .Select(c => c);
                return clients.ToList();
            }
        }

        public IList<Client> GetClientsInDistrict(string district)
        {
            using (context = new CustomDBContext())
            {
                var currentUserId = Utils.getInstance.GetCurrentUserId();

                var clients = context.Clients
                           .Where(c => c.ClientDistrict.Equals(district))
                           .Select(c => c);
                return clients.ToList();
            }
        }

        public Client GetClient(int clientId)
        {
            using (context = new CustomDBContext())
            {
                var client = context.Clients
                           .Where(c => c.ClientId == clientId)
                           .Select(c => c)
                           .FirstOrDefault();
                return client;
            }
        }
    }
}