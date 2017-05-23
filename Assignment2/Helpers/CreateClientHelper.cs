using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using System;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class CreateClientHelper
    {
        private IDao dao;

        public CreateClientHelper()
        {
            dao = new Dao();
        }

        //get the district for site manager
        public string GetDistrictForSiteManager ()
        {
            string district = dao.GetUserDistrict(Utils.getInstance.GetCurrentUserId());
            dao.GetClient();
            return district;
        }

        //This Function here creates a client
        public void CreateClient (string clientName, string clientLocation, string clientDistrict)
        {
            var client = new Client();
            client.ClientName = clientName;
            client.ClientLocation = clientLocation;
            client.ClientDistrict = clientDistrict;
            client.CreateDate = DateTime.Now;
            client.CreatedByUserId = Utils.getInstance.GetCurrentUserId();
            try
            {
                dao.AddClient(client.CreatedBy, client);
            } catch (Exception ex)
            {
                throw new FailedToCreateRecordException();
            }
            //var clientRepo = new ClientRepository(context);
            //Clients client = new Clients();
            //client.ClientName = clientName;
            //client.ClientLocation = clientLocation;
            //client.ClientDistrict = clientDistrict;
            //var clientId = clientRepo.InsertWithGetId(client);
            //if (Utils.getInstance.isNullOrEmpty(clientId) || clientId == 0)
            //{
            //    throw new FailedToCreateRecordException();
            //}
            //var engineerClientRepo = new EngineerClientsRepository(context);
            //EngineersClients engClient = new EngineersClients();
            //engClient.UserId = HttpContext.Current.User.Identity.GetUserId();
            //engClient.ClientId = clientId;
            //engClient.CreateDate = DateTime.Now;
            //engineerClientRepo.Insert(engClient);
        }
    }
}