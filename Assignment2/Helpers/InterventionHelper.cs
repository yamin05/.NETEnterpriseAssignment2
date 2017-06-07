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
    public class InterventionHelper : IInterventionHelper
    {
        private IUserDao UDao = new UserDao();
        private IInterventionsDao interventionDao = new InterventionsDao();
        public InterventionHelper(IUserDao UDao, IInterventionsDao InterDao)
        {
            this.UDao=UDao;
            this.interventionDao = InterDao;

        }
        public InterventionHelper()
        {

        }

        /// <summary>
        /// This method use GetUser method to get Site Engineers Data
        /// </summary>
        /// <param name="userId">Id of the current Site Engineer</param>
        /// <returns>User</returns>
        public virtual User GetSiteEngineerData(string userId)
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
        public virtual string ValidateInterventionStatus(decimal requiredHours, decimal requiredCost,User siteEngineer)
        { 
            decimal userMaxCost = siteEngineer.MaximumCost;
            decimal userMaxHours = siteEngineer.MaximumHours;

            if (userMaxCost >= requiredCost  && userMaxHours >= requiredHours)
            {

                return Status.APPROVED;
            }
            else
            {
                return Status.PROPOSED;
            }

        }

        /// <summary>
        /// This Method is used to Create a new intevention 
        /// </summary>
        /// <param name="clientId">Id of an client this intervention is for</param>
        /// <param name="interventionTypeId">Id of an Intervention type</param>
        /// <param name="interventionCost">Cost required to complete intervention</param>
        /// <param name="interventionHours">Hours required to complete intervention</param>
        public void CreateIntervention(int clientId, int interventionTypeId, decimal interventionCost, decimal interventionHours, string comments)
        {
            User siteEngineer = GetSiteEngineerData(Utils.getInstance.GetCurrentUserId());
            var intervention = new Intervention();
            string status = ValidateInterventionStatus(interventionHours, interventionCost, siteEngineer);
            if (status.Equals(Status.APPROVED)) {
                intervention.Status = Status.APPROVED;
            }
            else
            {
                intervention.Status = Status.PROPOSED;
            }

            intervention.ClientId = clientId;
            intervention.InterventionTypeId = interventionTypeId;
            intervention.InterventionCost = interventionCost;
            intervention.InterventionHours = interventionHours;
            intervention.CreatedByUserId = Utils.getInstance.GetCurrentUserId();
            intervention.CreateDate = DateTime.Now;
            intervention.Comments = comments;


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
        public IList<ListInterventionViewModel> ListInterventions()
        {
            try
            {
                var userId = Utils.getInstance.GetCurrentUserId();
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var interventions = interventionDao.GetInterventionsForUser(userId);

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

        /// <summary>
        /// This method is to list all interventions assosiated with the Client
        /// </summary>
        /// <returns>IList</returns>
        public IList<ListInterventionViewModel> ListOfClientsInterventions(int id)
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
            InterventionView.Status = Inter.Status;
            InterventionView.CreateDate = Inter.CreateDate;
            InterventionView.InterventionId = Inter.InterventionId;
            InterventionView.ModifyDate = Inter.ModifyDate;
            InterventionView.Condition = Inter.Condition;
            InterventionView.comments = Inter.Comments;
            return InterventionView;
        }



        public IList<Intervention> ListofProposedIntervention()
        {
            IList<Intervention> ProposedInterList = new List<Intervention>();
            ProposedInterList = interventionDao.GetInterventionsByStatus(Status.PROPOSED);
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
                    ViewIntervention.Status = inter.Status;
                    ViewList.Add(ViewIntervention);
                }
                return ViewList;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }

        }
        public Dictionary<string, string> GetPossibleStatusUpdateForInterventionForSiteEngineer(string status)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            if (Status.PROPOSED.Equals(status))
            {   /*list.Clear();*/
                list.Add(Status.PROPOSED, Status.PROPOSED);
                list.Add(Status.CANCELLED, Status.CANCELLED);
            }
            else if (Status.APPROVED.Equals(status))
            {
                //list.Clear();
                list.Add(Status.APPROVED, Status.APPROVED);
                list.Add(Status.COMPLETED, Status.COMPLETED);
                list.Add(Status.CANCELLED, Status.CANCELLED);
            }
            else if (Status.COMPLETED.Equals(status))
            {
                //list.Clear();
                list.Add(Status.COMPLETED, Status.COMPLETED);
            }

            return list;
        }

        public Dictionary<string, string> GetPossibleStatusUpdateForIntervention(string status)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            if (Status.PROPOSED.Equals(status))
            {   /*list.Clear();*/
                list.Add(Status.PROPOSED, Status.PROPOSED);
                list.Add(Status.APPROVED, Status.APPROVED);
                list.Add(Status.CANCELLED, Status.CANCELLED);
            }
            else if (Status.APPROVED.Equals(status))
            {
                //list.Clear();
                list.Add(Status.APPROVED, Status.APPROVED);
                list.Add(Status.CANCELLED, Status.CANCELLED);
            }
            else if (Status.CANCELLED.Equals(status))
            {

                list.Add(Status.CANCELLED, Status.CANCELLED);
            }

            return list;
        }

        public IList<ListInterventionViewModel> ListOfClientsInterventions(int? id)
        {
            throw new NotImplementedException();
        }
    }
}   

