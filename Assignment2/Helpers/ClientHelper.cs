using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class ClientHelper
    {
        private Dao clientDao;

        public ClientHelper() {

            clientDao = new Dao();
        }

        public IList<ListClientsViewModel> ListOfClients()
        {
            try
            {
                IList<Client> getList = new List<Client>();
                IList<ListClientsViewModel> ViewList = new List<ListClientsViewModel>();
                getList = clientDao.GetAllClientsForUser();

                foreach (var inter in getList)
                {
                    ListClientsViewModel ViewClients = new ListClientsViewModel();
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
    }
}