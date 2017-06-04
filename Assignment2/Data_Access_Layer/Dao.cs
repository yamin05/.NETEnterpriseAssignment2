using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System.Data.Entity;

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

        /// <summary>
        /// This method is used for getting all clients associated with the user
        /// </summary>
        /// <returns>IList</returns>
        public IList<Client> GetAllClientsForUser()
        {
            using (context = new CustomDBContext())
            {
                var currentUserId = Utils.getInstance.GetCurrentUserId();

                var clients = context.Clients
                           .Where(c => c.ClientDistrict.Equals(c.CreatedBy.District))
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

        /// <summary>
        /// This method is used for getting current users data
        /// </summary>
        /// <param name="userID">Id of the current User</param>
        /// <returns>user</returns>
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

        public IList<Intervention> GetInterventionsForManager(User manager)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> intervention = (context.Interventions
                                    .Where(i => i.Status == Status.PROPOSED && manager.MaximumHours >= i.InterventionHours 
                                            && manager.MaximumCost >= i.InterventionCost && manager.District.Equals(i.Client.ClientDistrict))
                                    .Select(i => i))
                                    .Include(i => i.Client)
                                    .Include(i => i.InterTypeId)
                                    .ToList();
                return intervention;
            }
        }

        public Intervention GetIntervention(int InterventionId)
        {
            using (context = new CustomDBContext())
            {
                Intervention intervention = context.Interventions
                                    .Where(i => i.InterventionId == InterventionId)
                                    .Select(i => i)
                                    .Include(i => i.Client)
                                    .Include(i => i.InterTypeId)
                                    .FirstOrDefault();
                return intervention;
            }
        }

        public Intervention UpdateIntervention(int interventionId, User user, string oldStatus, string newStatus)
        {
            using (context = new CustomDBContext())
            {
                var intervention = context.Interventions
                                   .Where(i => i.InterventionId == interventionId && i.Client.ClientDistrict.Equals(user.District)
                                           && i.Status.Equals(oldStatus))     //Added &&
                                   .Select(i => i)
                                   .FirstOrDefault();
                if (newStatus.Equals(Status.APPROVED))
                {
                    intervention.ApprovedByUserId = user.UserId;
                }
                intervention.LastUpdatedByUserId = user.UserId;
                intervention.Status = newStatus;
                intervention.ModifyDate = DateTime.Now;
                context.SaveChanges();
                return intervention;
            }
        }
    }
}