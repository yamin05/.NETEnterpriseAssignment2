﻿using System;
using System.Linq;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using WebApplication2.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;

namespace Assignment2.Data_Access_Layer
{
    public class InterventionsDao : IInterventionsDao
    {
        private CustomDBContext context;
        public void AddIntervention(Intervention intervention)
        {
            using (context = new CustomDBContext())
            {
                context.Interventions.Add(intervention);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method is used for getting intervention Type from the Id
        /// </summary>
        /// <param name="interventionTypeId">Id of an intervention type</param>
        /// <returns>InterventionType</returns>
        public InterventionType GetInterventionType(int interventionTypeId)
        {
            using (context = new CustomDBContext())
            {

                var interventionType = context.InterventionTypes
                                        .Where(i => i.InterventionTypeId.Equals(interventionTypeId))
                                        .Select(i => i)
                                        .FirstOrDefault();

                return interventionType;
            }
        }

        /// <summary>
        /// This method is used for getting intervention with intervention id
        /// </summary>
        /// <returns>IList</returns>
        public Intervention GetIntervention(int InterventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention = context.Interventions
                                    .Where(i => i.InterventionId == InterventionId)
                                    .Select(i => i)
                                    .Include(i => i.Client)
                                    .Include(i => i.InterTypeId)
                                    .FirstOrDefault();
                return intervention;
            }
        }

        /// <summary>
        /// This method is used for getting inteventions created by the user
        /// </summary>
        /// <param name="userId">Id of a current User</param>
        /// <returns>IList</returns>
        public IList<Intervention> GetInterventionsForUser(string userId)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> interventions = context.Interventions
                                            .Select(i => i)
                                            .Where(i => i.CreatedByUserId.Equals(userId) && i.Status != Status.CANCELLED)
                                            .Include(i => i.InterTypeId)
                                            .Include(i => i.Client)
                                            .ToList();
                return interventions;
            }
        }

        /// <summary>
        /// This method is used for getting client's assosiated inteventions
        /// </summary>
        /// <param name="Id">Id of the client</param>
        /// <returns>IList</returns>
        public IList<Intervention> GetClientsInterventions(int clientId)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> clientInterventions = context.Interventions
                                            .Select(i => i)
                                            .Where(i => i.ClientId == clientId && i.Status != Status.CANCELLED)
                                            .Include(i => i.InterTypeId)
                                            .ToList();
                return clientInterventions;
            }
        }


        /// <summary>
        /// This method is used for getting interventions with status
        /// </summary>
        /// <returns>IList</returns>
        public IList<Intervention> GetInterventionsByStatus(string status)
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

        public Intervention UpdateIntervention(Intervention intervention)
        {
            using (context = new CustomDBContext())
            {
                var inter = context.Interventions
                            .Where(i => i.InterventionId == intervention.InterventionId)
                            .Select(i => i)
                            .FirstOrDefault();
                inter.ModifyDate = DateTime.Now;
                inter.Status = intervention.Status;
                inter.LastUpdatedByUserId = intervention.LastUpdatedByUserId;
                inter.Comments = intervention.Comments;
                inter.Condition = intervention.Condition;
                inter.InterventionCost = intervention.InterventionCost;
                inter.InterventionHours = intervention.InterventionHours;
                context.SaveChanges();
                return inter;
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

        public IList<Intervention> GetAssociatedIntervention_ForManager(string user_Id)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> intervention = (context.Interventions
                                    .Where(i => i.ApprovedByUserId.Equals(user_Id) && i.Status == Status.APPROVED)
                                    .Select(i => i))
                                    .Include(i => i.Client)
                                    .Include(i => i.InterTypeId)
                                    .ToList();
                return intervention;
            }
        }
    }
}
