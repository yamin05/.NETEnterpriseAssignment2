using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Data_Access_Layer
{
    public interface IInterventionsDao
    {
        void AddIntervention(Intervention intervention);
        InterventionType GetInterventionType(int interventionTypeId);
        Intervention GetIntervention(int InterventionId);
        IList<Intervention> GetInterventionsForUser(string userId);
        IList<Intervention> GetClientsInterventions(int clientId);
        IList<Intervention> GetInterventionsByStatus(string status);
        Intervention UpdateIntervention(Intervention intervention);
        Intervention UpdateIntervention(int interventionId, User user, string oldStatus, string newStatus);
        IList<Intervention> GetAssociatedIntervention_ForManager(string user_Id);
    }
}
