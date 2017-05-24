using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment2.Models;
using Assignment2.Models.Database_Models;

namespace Assignment2.Data_Access_Layer
{
    public class Dao : IDao
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

        public IList<Client> GetAllClientsForUser(string userId)
        {
            using (context = new CustomDBContext())
            {
                var clients = context.Clients
                           .Where(c => c.CreatedByUserId.Equals(userId))
                           .Select(c => c);
                return clients.ToList();
            }
        }

        public Client GetClient()
        {
            using (context = new CustomDBContext())
            {
                var client = context.Clients
                           .Where(c => c.ClientId == 1)
                           .Select(c => c)
                           .FirstOrDefault();
                return client;
            }
        }

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