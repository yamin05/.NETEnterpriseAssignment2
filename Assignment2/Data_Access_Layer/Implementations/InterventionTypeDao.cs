using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment2.Models.Database_Models;

namespace Assignment2.Data_Access_Layer
{
    public class InterventionTypeDao : IInterventionTypeDao
    {
        private CustomDBContext context;
        public IList<InterventionType> GetAllInterventionTypes()
        {
            using (context = new CustomDBContext())
            {
                var interventionTypes = context.InterventionTypes;
                return interventionTypes.ToList();
            }
        }

        public InterventionType GetIntervention(int interventionTypeId)
        {
            using (context = new CustomDBContext())
            {
                var interventionTypes = context.InterventionTypes
                                        .Where(i => i.InterventionTypeId == interventionTypeId)
                                        .Select(i => i);
                return interventionTypes.FirstOrDefault();
            }
        }
    }
}