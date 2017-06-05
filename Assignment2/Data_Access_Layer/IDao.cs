using Assignment2.Models.Database_Models;
using System.Collections.Generic;

namespace Assignment2.Data_Access_Layer
{
    public interface IDao
    {
        User GetUser(string userID);
        string GetUserDistrict(string userId);
        void AddClient(User user, Client client);
        Client GetClient(int clientId);
        IList<Client> GetAllClientsForUser();
        IList<Intervention> GetInterventionsForManager(User manager);
        Intervention GetIntervention(int InterventionId);
        IList<Intervention> GetInterventions(string status);
        Intervention UpdateIntervention(int interventionId, User user, string oldStatus, string newStatus);
    }
}
