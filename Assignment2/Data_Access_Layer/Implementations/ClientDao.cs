using Assignment2.Models.Database_Models;
using System.Collections.Generic;
using System.Linq;
using System;

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


        /// <summary>
        /// This method is used for getting all clients associated with the district
        /// </summary>
        /// <returns>IList</returns>
        public IList<Client> GetClientsInDistrict(string district)
        {
            using (context = new CustomDBContext())
            {
                var clients = context.Clients
                           .Where(c => c.ClientDistrict.Equals(district))
                           .Select(c => c);
                return clients.ToList();
            }
        }


        /// <summary>
        /// This method is used for getting a client detail based on the clientID
        /// </summary>
        /// <returns>IList</returns>
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

        /// <summary>
        /// This method is used for getting a client detail in the same district based on the userid and district
        /// </summary>
        /// <returns>IList</returns>
        public IList<Client> GetCurrentClients(string userId, string district)
        {
            using (context = new CustomDBContext())
            {
                var clients = context.Clients
                           .Where(c => c.ClientDistrict.Equals(district) && c.CreatedByUserId.Equals(userId))
                           .Select(c => c);
                return clients.ToList();
            }
        }
    }
}