using System;
using System.Linq;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using WebApplication2.Exceptions;
using System.Collections.Generic;

namespace Assignment2.Data_Access_Layer
{
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

        public Intervention GetIntervention(int? InterventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention = context.Interventions
                                    .Where(i => i.InterventionId.Equals(InterventionId))
                                    .Select(i => i)
                                    .FirstOrDefault();
                return intervention;

            }
        }

        public IList<Intervention> GetInterventionsByStatus(int status)
        {
            using (context = new CustomDBContext())
            {
                IList<Intervention> intervention = (context.Interventions
                                    .Where(i => i.Status.Equals(status))
                                    .Select(i => i)).ToList();
                                   
                return intervention;

            }
        }

        public void UpdateInterventionStatus_ToAppoved(int interventionId)
        {
            using (context = new CustomDBContext())
            {
                var intervention =
                from inter in context.Interventions
                where inter.Status == Status.PROPOSED && inter.InterventionId == interventionId     //Added &&
                select inter;
                foreach (Intervention inter in intervention)
                {
                    inter.Status = Status.APPROVED;
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
}