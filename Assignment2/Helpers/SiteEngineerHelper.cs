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
        private IDao dao;

        public SiteEngineerHelper()
        {
            dao = new Dao();
        }

        /// <summary>
        /// This method is for getting district of the current User
        /// </summary>
        /// <returns>string</returns>
        public string GetDistrictForSiteManager ()
        {
            try
            {
                string district = dao.GetUserDistrict(Utils.getInstance.GetCurrentUserId());
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
                dao.AddClient(client.CreatedBy, client);
            } catch (Exception)
            {
                throw new FailedToCreateRecordException();
            }
        }



     /** No Need for following code for retriving Clients from DB !!
      
        //public IList<GetAllClientsViewModel> GetAllClientsForUser()
        //{
        //    IList<GetAllClientsViewModel> viewModels = new List<GetAllClientsViewModel>();
        //    try
        //    {
        //        var clients = dao.GetAllClientsForUser(Utils.getInstance.GetCurrentUserId());
        //        foreach (var client in clients)
        //        {
        //            var viewModel = new GetAllClientsViewModel();
        //            viewModel.clientID = client.ClientId;
        //            viewModel.clientName = client.ClientName;
        //            viewModel.clientLocation = client.ClientLocation;
        //            viewModel.clientDistrict = client.ClientDistrict;
        //            viewModels.Add(viewModel);
        //        }
        //        return viewModels;
        //    }
        //    catch (Exception)
        //    {
        //        throw new FaliedToRetriveRecordException();
        //    }
        //}
    **/
    }
}