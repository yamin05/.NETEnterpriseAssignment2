using Assignment2.Models.Database_Models;

namespace Assignment2.Helpers
{
    public interface IMailHelper
    {
        void SendMail(string from, string to, Intervention intervention, string newStatus);
    }
}