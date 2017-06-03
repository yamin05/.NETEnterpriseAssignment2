using System;
using System.Linq;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using WebApplication2.Exceptions;
using System.Collections.Generic;
using System.Data.Entity;
using Assignment2.Data_Access_Layer;

public class InterventionsDao
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
    public InterventionType GetInterventionType(int interventionTypeId) {

        using (context = new CustomDBContext()) {

            var interventionType = context.InterventionTypes
                                    .Where(i => i.InterventionTypeId.Equals(interventionTypeId))
                                    .Select(i => i)
                                    .FirstOrDefault();

            return interventionType;
        }

    }

    public Intervention GetIntervention(int? InterventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention = context.Interventions
                                    .Where(i => i.InterventionId == InterventionId)
                                    .Select(i => i)
                                    .Include(i => i.Client)
                                    .Include(i=> i.InterTypeId)
                                    .FirstOrDefault();
                return intervention;

            }
        }

    /// <summary>
    /// This method is used for getting inteventions created by the user
    /// </summary>
    /// <param name="userId">Id of a current User</param>
    /// <returns>IList</returns>
    public IList<Intervention> GetUsersInterventions(string userId) {

        using (context = new CustomDBContext()) {

           IList<Intervention> usersInterventions = context.Interventions
                                       .Select(i => i)
                                       .Where(i => i.CreatedBy.UserId.Equals(userId) && i.Status != (int)Status.Cancelled)
                                       .Include(i => i.InterTypeId).ToList();

            return usersInterventions;
        

        }

    }

    /// <summary>
    /// This method is used for getting client's assosiated inteventions
    /// </summary>
    /// <param name="Id">Id of the client</param>
    /// <returns>IList</returns>
    public IList<Intervention> GetClientsInterventions(int? id)
    {

        using (context = new CustomDBContext())
        {

            IList<Intervention> clientInterventions = context.Interventions
                                        .Select(i => i)
                                        .Where(i => i.ClientId == id && i.Status != (int)Status.Cancelled)
                                        .Include(i => i.InterTypeId).ToList();

            return clientInterventions;


        }

    }
    public IList<Intervention> GetInterventionsByStatus(int status)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> intervention = (context.Interventions
                                    .Where(i => i.Status.Equals(status))
                                    .Select(i => i))
                                    .Include(i=> i.Client)
                                    .Include(i=>i.InterTypeId)
                                    .ToList();
                                    
                                   
                return intervention;

            }
        }

    public void UpdateLife(int interventionId, int? life) {
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
                    inter.Status = (int)Status.Approved;
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
                inter.Status = (int)Status.Cancelled;
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

}
