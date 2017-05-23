using Assignment2.Models.Database_Models;

namespace Assignment2.Data_Access_Layer
{
    public interface IDao
    {
        User GetUser(string userID);
        string GetUserDistrict(string userId);
        void AddClient(User user, Client client);
        Client GetClient();
    }
}
