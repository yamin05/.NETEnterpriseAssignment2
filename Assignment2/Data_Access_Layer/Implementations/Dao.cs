using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System.Data.Entity;
using WebApplication2.Exceptions;

namespace Assignment2.Data_Access_Layer
{
    public class Dao : IDao
    {
        public IClientDao client { get; set; }
        public IInterventionsDao intervention { get; set; }
        public IInterventionTypeDao interventionType { get; set; }
        public IUserDao user { get; set; }

        public Dao() {
            client = new ClientDao();
            intervention = new InterventionsDao();
            interventionType = new InterventionTypeDao();
            user = new UserDao();
            }

        private CustomDBContext context;

        /// <summary>
        /// This method is used for getting all intervention for manager based on manger
        /// </summary>
        /// <returns>IList</returns>
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
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    throw new FailedToUpdateRecordException();
                }
                return intervention;
            }
        }

        public IList<Intervention> GetInterventions(string status)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> intervention = (context.Interventions
                                    .Where(i => i.Status.Equals(status))
                                    .Select(i => i))
                                    .Include(i => i.Client)
                                    .Include(i => i.InterTypeId)
                                    .ToList();
                return intervention;
            }
        }
    }
}