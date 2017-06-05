using Assignment2.Models.Database_Models;
using System.Collections.Generic;

namespace Assignment2.Data_Access_Layer
{
    public interface IDao
    {    
        IList<Intervention> GetInterventionsForManager(User manager);
        Intervention GetIntervention(int InterventionId);
        IList<Intervention> GetInterventions(string status);
        Intervention UpdateIntervention(int interventionId, User user, string oldStatus, string newStatus);
    }
}
