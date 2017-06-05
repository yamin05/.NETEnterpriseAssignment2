using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class SiteEngineerHelper
    {
        private Dao dao = new Dao();

        /// <summary>
        /// This method is for getting district of the current User
        /// </summary>
        /// <returns>string</returns>
        public string GetDistrictForSiteManager ()
        {
            try
            {
                string district = dao.user.GetUserDistrict(Utils.getInstance.GetCurrentUserId());
                return district;
            }
            catch (Exception)
            {
                throw new FaliedToRetriveRecordException();
            }
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
                dao.client.AddClient(client.CreatedBy, client);
            } catch (Exception)
            {
                throw new FailedToCreateRecordException();
            }
        }

        public IList<ListClientsViewModel> ListOfClients()
        {
            try
            {
                IList<ListClientsViewModel> ViewList = new List<ListClientsViewModel>();
                var clients = dao.client.GetClientsForUser(Utils.getInstance.GetCurrentUserId());

                foreach (var inter in clients)
                {
                    ListClientsViewModel ViewClients = new ListClientsViewModel();
                    ViewClients.clientID = inter.ClientId;
                    ViewClients.clientName = inter.ClientName;
                    ViewClients.clientLocation = inter.ClientLocation;
                    ViewClients.createDate = inter.CreateDate;
                    ViewList.Add(ViewClients);
                }

                return ViewList;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }

        public IList<ListClientsViewModel> ListOfClientsInDistrict()
        {
            try
            {
                IList<ListClientsViewModel> ViewList = new List<ListClientsViewModel>();
                var district = this.GetDistrictForSiteManager();
                var clients = dao.client.GetClientsInDistrict(district);

                foreach (var inter in clients)
                {
                    ListClientsViewModel ViewClients = new ListClientsViewModel();
                    ViewClients.clientID = inter.ClientId;
                    ViewClients.clientName = inter.ClientName;
                    ViewClients.clientLocation = inter.ClientLocation;
                    ViewClients.createDate = inter.CreateDate;
                    ViewClients.createdBy = Utils.getInstance.GetIdentityUser(inter.CreatedByUserId).UserName;
                    ViewList.Add(ViewClients);
                }

                return ViewList;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }
    }
}