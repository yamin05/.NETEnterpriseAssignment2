using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public  class SiteEngineerHelper : ISiteEngineerHelper
    {
        private Dao dao = new Dao();
        private IUserDao uDao = new UserDao();
        private IInterventionsDao interventionDao = new InterventionsDao();

        public SiteEngineerHelper(IUserDao object1, IInterventionsDao object2)
        {
            uDao = object1;
            interventionDao = object2;
        }

        public SiteEngineerHelper()
        {
        }

        public virtual User GetSiteEngineerData(string userId)
        {
            User siteEngineer = uDao.GetUser(userId);
            return siteEngineer;
        }

        public InterventionType GetInterventionTypeData(int interventionTypeId)
        {
            InterventionType interventionType = interventionDao.GetInterventionType(interventionTypeId);
            return interventionType;
        }

        /// <summary>
        /// This method is for getting district of the current User
        /// </summary>
        /// <returns>string</returns>
        public string GetDistrictForUser ()
        {
            try
            {
                string district = uDao.GetUserDistrict(Utils.getInstance.GetCurrentUserId());
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
                var district = this.GetDistrictForUser();
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

        /// <summary>
        /// This method is to list all interventions assosiated with the user
        /// </summary>
        /// <returns>IList</returns>
        public IList<ListInterventionViewModel> ListInterventions()
        {
            try
            { 
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var interventions = interventionDao.GetInterventionsForUser(Utils.getInstance.GetCurrentUserId());

                foreach (var inter in interventions)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
                    ViewIntervention.Status = inter.Status;
                    ViewIntervention.Condition = inter.Condition;
                    ViewIntervention.ModifyDate = inter.ModifyDate;
                    ViewList.Add(ViewIntervention);
                }

                return ViewList;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }

        public IList<Client> ListCurrentClients()
        {
            var clients = dao.client.GetCurrentClients(Utils.getInstance.GetCurrentUserId(), this.GetDistrictForUser());
            if (Utils.getInstance.isNullOrEmpty(clients))
            {
                throw new NoClientCreatedException();
            }
            return clients;
        }

        public IList<InterventionType> ListInterventionTypes()
        {
            var interventionTypes = dao.interventionType.GetAllInterventionTypes();
            return interventionTypes;
        }

        /// <summary>
        /// This Method is used to Create a new intevention 
        /// </summary>
        /// <param name="clientId">Id of an client this intervention is for</param>
        /// <param name="interventionTypeId">Id of an Intervention type</param>
        /// <param name="interventionCost">Cost required to complete intervention</param>
        /// <param name="interventionHours">Hours required to complete intervention</param>
        public void CreateIntervention(int clientId, int interventionTypeId, decimal interventionCost, decimal interventionHours)
        {
            var intervention = new Intervention();
            var siteEngineer = GetSiteEngineerData(Utils.getInstance.GetCurrentUserId());
            intervention.Status = ValidateInterventionStatus(interventionHours, interventionCost, siteEngineer);
            intervention.ClientId = clientId;
            intervention.InterventionTypeId = interventionTypeId;
            intervention.InterventionCost = interventionCost;
            intervention.InterventionHours = interventionHours;
            intervention.CreatedByUserId = Utils.getInstance.GetCurrentUserId();
            intervention.Comments = String.Empty;
            intervention.CreateDate = DateTime.Now;
            try
            {
                interventionDao.AddIntervention(intervention);
            }
            catch (Exception)
            {
                throw new FailedToCreateRecordException();
            }
        }

        public string ValidateInterventionStatus(decimal requiredHours, decimal requiredCost, User siteEngineer)
        {
            decimal userMaxCost = siteEngineer.MaximumCost;
            decimal userMaxHours = siteEngineer.MaximumHours;

            if (userMaxCost >= requiredCost && userMaxHours >= requiredHours)
            {

                return Status.APPROVED;
            }
            else
            {
                return Status.PROPOSED;
            }

        }

        public IList<ListInterventionViewModel> ListOfClientsInterventions(int clientId)
        {
            try
            {
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var interventions = interventionDao.GetClientsInterventions(clientId);

                foreach (var inter in interventions)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
                    ViewIntervention.CreatedBy = Utils.getInstance.GetIdentityUser(inter.CreatedByUserId).UserName;
                    ViewIntervention.Status = inter.Status;
                    ViewIntervention.Condition = inter.Condition;
                    ViewIntervention.ModifyDate = inter.ModifyDate;
                    ViewList.Add(ViewIntervention);
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