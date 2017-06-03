using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;
using System.Web;
using Microsoft.AspNet.Identity;



namespace Assignment2.Helpers
{
    /// <summary>
    /// This class is act as a helper for creating, listing, validating,  updating Interventions
    /// </summary>
    public class InterventionHelper
    {
        private Dao UDao;
        private InterventionsDao interventionDao;
        public InterventionHelper()
        {
            UDao = new Dao();
            interventionDao = new InterventionsDao();
        }

        /// <summary>
        /// This method use GetUser method to get Site Engineers Data
        /// </summary>
        /// <param name="userId">Id of the current Site Engineer</param>
        /// <returns>User</returns>
        public User GetSiteEngineerData(string userId)
        {
            User siteEngineer = UDao.GetUser(userId);
            return siteEngineer;
        }

        /// <summary>
        /// This method use GetInterventionType method to get Intervention Type Data i.e Default Coast and Hours
        /// </summary>
        /// <param name="interventionTypeId">Id of an Intervention type</param>
        /// <returns>InterventionType</returns>
        public InterventionType GetInterventionTypeData(int interventionTypeId) {

            InterventionType interventionType = interventionDao.GetInterventionType(interventionTypeId);
            return interventionType;
        }

        /// <summary>
        /// This method is used to check if created intervention should be Approved or marked as Proposed based on User and Intervention's Data
        /// </summary>
        /// <param name="interventionTypeId">Id of an intervention type</param>
        /// <param name="requiredHours">Hours required to complete intervention enterd by Site Engineer</param>
        /// <param name="requiredCost">Cost required to complete intervention enterd by Site Engineer</param>
        /// <returns>string</returns>
        public string ValidateInterventionStatus(int interventionTypeId, decimal requiredHours, decimal requiredCost) {

            InterventionType interventionType = GetInterventionTypeData(interventionTypeId);
            decimal defaultCost = interventionType.InterventionTypeCost;
            decimal defaultHours = interventionType.InterventionTypeHours;

            User siteEngineer = GetSiteEngineerData(Utils.getInstance.GetCurrentUserId());
            decimal userMaxCost = siteEngineer.MaximumCost;
            decimal userMaxHours = siteEngineer.MaximumHours;

            if (userMaxCost >= defaultCost && userMaxCost >= requiredCost && userMaxHours >= defaultHours && userMaxHours >= requiredHours)
            {

                return Status.Approved.ToString();
            }
            else {
                return Status.Proposed.ToString();
            }

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
            string status = ValidateInterventionStatus(interventionTypeId, interventionHours, interventionCost);
            if (status == Status.Approved.ToString()) {
                intervention.Status = Convert.ToInt32(Status.Approved);
            }
            else
            {
                intervention.Status = Convert.ToInt32(Status.Proposed);
            }

            intervention.ClientId = clientId;
            intervention.InterventionTypeId = interventionTypeId;
            intervention.InterventionCost = interventionCost;
            intervention.InterventionHours = interventionHours;
            intervention.CreatedByUserId = Utils.getInstance.GetCurrentUserId();
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

        /// <summary>
        /// This method is to list all interventions assosiated with the user
        /// </summary>
        /// <returns>IList</returns>
        public IList<ListInterventionViewModel> ListOfUsersInterventions()
        {
            try
            {
                var userId = Utils.getInstance.GetCurrentUserId();
                IList<Intervention> getList = new List<Intervention>();
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                getList = interventionDao.GetUsersInterventions(userId);

                foreach (var inter in getList)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
                    ViewIntervention.Status = Enum.GetName((typeof(Status)), inter.Status);
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

        /// <summary>
        /// This method is to list all interventions assosiated with the Client
        /// </summary>
        /// <returns>IList</returns>
        public IList<ListInterventionViewModel> ListOfClientsInterventions(int? id)
        {
            try
            {
                IList<Intervention> getList = new List<Intervention>();
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                getList = interventionDao.GetClientsInterventions(id);

                foreach (var inter in getList)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
                    ViewIntervention.Status = Enum.GetName((typeof(Status)), inter.Status);
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

        public ListInterventionViewModel GetIntervention(int? userid)
        {
            Intervention Inter = new Intervention();
            Inter = interventionDao.GetIntervention(userid);
            ListInterventionViewModel InterventionView = new ListInterventionViewModel();
            InterventionView.ClientDistrict = Inter.Client.ClientDistrict;
            InterventionView.ClientName = Inter.Client.ClientName;
            InterventionView.InterventionTypeName = Inter.InterTypeId.InterventionTypeName;
            InterventionView.InterventionCost = Inter.InterventionCost;
            InterventionView.InterventionHours = Inter.InterventionHours;
            InterventionView.Status = Enum.GetName((typeof(Status)), Inter.Status);
            InterventionView.CreateDate = Inter.CreateDate;
            InterventionView.InterventionId = Inter.InterventionId;
            InterventionView.ModifyDate = Inter.ModifyDate;
            InterventionView.Condition = Inter.Condition;
            return InterventionView;
        }



        public IList<Intervention> ListofProposedIntervention()
        {
            IList<Intervention> ProposedInterList = new List<Intervention>();
            ProposedInterList = interventionDao.GetInterventionsByStatus((int)Status.Proposed);
            return ProposedInterList;
        }

        public IList<Intervention> ValidateProposedInterventions(User manager, IList<Intervention> InterList)
        {
            IList<Intervention> ProposedList = new List<Intervention>();
            for (int i = 0; i <= InterList.Count - 1; i++)
            {
                {
                    if (manager.MaximumHours >= InterList[i].InterventionHours && manager.MaximumCost >= InterList[i].InterventionCost && manager.District == InterList[i].Client.ClientDistrict)
                    {
                        ProposedList.Add(InterList[i]);

                    }
                }
            }
            return ProposedList;

        }

        public User GetManagerData(string userId)
        {
            User manager = UDao.GetUser(userId.ToString());
            return manager;
        }
        public IList<ListInterventionViewModel> ListOfProposedInterventionsForManager()
        {
            try
            {
                IList<Intervention> interlist = new List<Intervention>();
                interlist = ListofProposedIntervention();
                var ManageruserId = HttpContext.Current.User.Identity.GetUserId();
                IList<Intervention> proposedinterlist = new List<Intervention>();
                IList<ListInterventionViewModel> ViewList=new List<ListInterventionViewModel>();
                var manager = GetManagerData(ManageruserId);
                proposedinterlist = ValidateProposedInterventions(manager, interlist);
                foreach(var inter in proposedinterlist)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.ClientDistrict = inter.Client.ClientDistrict;
                    ViewIntervention.ClientName = inter.Client.ClientName;
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
                    ViewIntervention.Status = Enum.GetName((typeof(Status)),inter.Status);
                    ViewList.Add(ViewIntervention);
                }
                return ViewList;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }

        }
        public Dictionary<string, int> GetPossibleStatusUpdateForInterventionForSiteEngineer(string status)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            if (Status.Proposed.ToString().Equals(status))
            {   /*list.Clear();*/
                list.Add(Status.Proposed.ToString(), (int)Status.Proposed);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            else if (Status.Approved.ToString().Equals(status))
            {
                //list.Clear();
                list.Add(Status.Approved.ToString(), (int)Status.Approved);
                list.Add(Status.Completed.ToString(), (int)Status.Completed);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            else if (Status.Completed.ToString().Equals(status))
            {
                //list.Clear();
                list.Add(Status.Completed.ToString(), (int)Status.Completed);
            }

            return list;
        }

        public Dictionary<string, int> GetPossibleStatusUpdateForIntervention(string status)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            if (Status.Proposed.ToString().Equals(status))
            {   /*list.Clear();*/
                list.Add(Status.Proposed.ToString(), (int)Status.Proposed);
                list.Add(Status.Approved.ToString(), (int)Status.Approved);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            else if (Status.Approved.ToString().Equals(status))
            {
                //list.Clear();
                list.Add(Status.Approved.ToString(), (int)Status.Approved);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            else if (Status.Cancelled.ToString().Equals(status))
            {

                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }

            return list;
        }



    }
}   

