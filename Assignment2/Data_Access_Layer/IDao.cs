using Assignment2.Models.Database_Models;
using System.Collections.Generic;

namespace Assignment2.Data_Access_Layer
{
    public interface IDao
    {
        User GetUser(string userID);
        string GetUserDistrict(string userId);
        void AddClient(User user, Client client);
        Client GetClient();
        IList<Client> GetAllClientsForUser(string userId);
    }
}
