using Assignment2.Models.Database_Models;
using System;
using System.Configuration;
using System.Net.Mail;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class MailHelper : IMailHelper
    {
        private SmtpClient smtp { get; set; }

        public MailHelper()
        {
            smtp = new SmtpClient()
            {
                Host = ConfigurationManager.AppSettings["smtpServer"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]),
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]),
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = "C:\\Users\\Public"
            };
        }

        public void SendMail(string from, string to, Intervention intervention, string newStatus)
        {
            var fromUser = Utils.getInstance.GetIdentityUser(from);
            var toUser = Utils.getInstance.GetIdentityUser(to);
            MailMessage message = new MailMessage();
            MailAddress sender = new MailAddress(fromUser.Email);
            MailAddress receiver = new MailAddress(toUser.Email);
            message.From = sender;
            message.To.Add(receiver);
            message.Body = "Hello " + toUser.UserName + ",<br/>Your Intervention for Client: " + intervention.Client.ClientName 
                + " has been updated from <b>" + intervention.Status + "</b> to <b>" + newStatus + "</b> by " + fromUser.UserName
                + " on " + intervention.ModifyDate.ToString();
            message.IsBodyHtml = true;
            try
            {
                smtp.Send(message);
            }
            catch
            {
                throw new MailSendException();
            }
        }
    }
}