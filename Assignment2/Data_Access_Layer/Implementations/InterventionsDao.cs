using System;
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
        public Intervention GetIntervention(int? InterventionId)
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

        /// <summary>
        /// This method is used to change condition of an Intervention
        /// </summary>
        /// <param name="interventionId">id of an intervention</param>
        /// <param name="life">current life of an intervention</param>
        public void UpdateLife(int interventionId, int? life)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.InterventionId == interventionId
                select inter;
                foreach (Intervention inter in intervention)
                {
                    if (inter.Condition != life)
                    {
                        inter.Condition = life;
                        inter.ModifyDate = DateTime.Now;
                    }
                    else
                    {
                        inter.Condition = life;
                    }
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }


        }

        /// <summary>
        /// This method is used to change comment of an Intervention
        /// </summary>
        /// <param name="interventionId">id of an intervention</param>
        /// <param name="life">current comment of an intervention</param>
        public void UpdateComments(int interventionId, string comments)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.InterventionId == interventionId
                select inter;
                foreach (Intervention inter in intervention)
                {
                    if (!inter.Comments.Equals(comments))
                    {
                        inter.Comments = comments;
                        inter.ModifyDate = DateTime.Now;
                    }
                    else
                    {
                        inter.Comments = comments;
                    }
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }


        }

        /// <summary>
        /// This method is used for change intervention status to Approved
        /// </summary>
        /// <param name="interventionId">Id of an intervention</param>
        public void UpdateInterventionStatus_ToAppoved(int interventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.InterventionId == interventionId     //Added &&
                select inter;
                foreach (Intervention inter in intervention)
                {
                    inter.Status = Status.APPROVED;
                    inter.ModifyDate = DateTime.Now;
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }
        }

        /// <summary>
        /// This method is used for change intervention status to Completed
        /// </summary>
        /// <param name="interventionId">Id of an intervention</param>
        public void UpdateInterventionStatus_ToCompleted(int interventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.InterventionId == interventionId     //Added &&
                select inter;
                foreach (Intervention inter in intervention)
                {
                    inter.Status = Status.COMPLETED;
                    inter.ModifyDate = DateTime.Now;
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }
        }

        /// <summary>
        /// This method is used for change intervention status to Cancelled
        /// </summary>
        /// <param name="interventionId">Id of an intervention</param>
        public void UpdateInterventionStatus_ToCancelled(int interventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.InterventionId == interventionId     //Added &&
            select inter;
                foreach (Intervention inter in intervention)
                {
                    inter.Status = Status.CANCELLED;
                    inter.ModifyDate = DateTime.Now;
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FailedToUpdateRecordException();
                }

            }
        }

        public void UpdateIntervention(Intervention intervention)
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
            }
        }
    }
}
