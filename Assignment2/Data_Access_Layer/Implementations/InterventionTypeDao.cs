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

        /// <summary>
        /// This method is used for getting all intervention types
        /// </summary>
        public IList<InterventionType> GetAllInterventionTypes()
        {
            using (context = new CustomDBContext())
            {
                var interventionTypes = context.InterventionTypes;
                return interventionTypes.ToList();
            }
        }

        /// <summary>
        /// This method is used for getting intervention with intervention id
        /// </summary>
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