using Assignment2.Models.Database_Models;
using System.Collections.Generic;

namespace Assignment2.Data_Access_Layer
{
    public interface IClientDao
    {
        void AddClient(User user, Client client);
        Client GetClient(int clientId);
        IList<Client> GetClientsForUser(string userId);
        IList<Client> GetClientsInDistrict(string district);
    }
}
