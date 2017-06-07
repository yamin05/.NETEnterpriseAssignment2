using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class ManagerHelper : IManagerHelper
    {
        private IUserDao userDao = new UserDao();
        private IInterventionsDao interventionDao = new InterventionsDao();
        private IClientDao clientDao = new ClientDao();
        private IInterventionTypeDao interventionTypeDao = new InterventionTypeDao();
        /// <summary>
        /// This method is for getting list of proposed interventions
        /// </summary>
        public IList<ListInterventionViewModel> GetListOfProposedInterventions()
        {
            try
            {
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var manager = userDao.GetUser(Utils.getInstance.GetCurrentUserId());
                var interlist = ListofProposedIntervention();
                var proposedinterlist = ValidateProposedInterventions(manager, interlist);
                foreach (var inter in proposedinterlist)
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

        /// <summary>
        /// This method is for listing of proposed interventions
        /// </summary>
        private IList<Intervention> ListofProposedIntervention()
        {
            IList<Intervention> ProposedInterList = new List<Intervention>();
            ProposedInterList = interventionDao.GetInterventionsByStatus(Status.PROPOSED);
            return ProposedInterList;
        }

        /// <summary>
        /// This method is for validate proposed interventions for manager with intervention list
        /// </summary>
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

        /// <summary>
        /// This method is for getting intervention with intervention id
        /// </summary>
        public ListInterventionViewModel GetIntervention(int interventionId)
        {
            try
            {
                Intervention Inter = new Intervention();
                Inter = interventionDao.GetIntervention(interventionId);
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
                return InterventionView;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }

        /// <summary>
        /// This method is for getting possible status update for intervention with status
        /// </summary>
        public IList<string> GetPossibleStatusUpdateForIntervention(string status)
        {
            IList<string> list = new List<string>();
            if (Status.PROPOSED.Equals(status))
            {
                list.Add(Status.PROPOSED);
                list.Add(Status.APPROVED);
                list.Add(Status.CANCELLED);
            }
            else if (Status.APPROVED.Equals(status))
            {
                //list.Add(Status.COMPLETED);
                list.Add(Status.CANCELLED);
            }
            else if (Status.CANCELLED.Equals(status))
            {
                list.Add(Status.CANCELLED);
            }
            else if (Status.COMPLETED.Equals(status))
            {
                list.Add(Status.COMPLETED);

            }

            return list;
        }

        /// <summary>
        /// This method is for update intervention status with intervention id and new status
        /// </summary>
        public Intervention UpdateInterventionStatus(int interventionId, string newStatus)
        {
            var intervention = interventionDao.GetIntervention(interventionId);
            if (intervention.Status.Equals(newStatus))
            {
                throw new CannotEditStatusException();
            }
            var manager = userDao.GetUser(Utils.getInstance.GetCurrentUserId());
            var inter = interventionDao.UpdateIntervention(interventionId, manager, intervention.Status, newStatus);
            var mailHelper = new MailHelper();
            mailHelper.SendMail(manager.UserId, intervention.CreatedByUserId, intervention, inter.Status);
            return inter;
        }
        public IList<ListInterventionViewModel> GetAssociatedIntervention_ForManager()
        {
            try
            {
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var manager_userid = Utils.getInstance.GetCurrentUserId();
                IList<Intervention> interlist = new List<Intervention>();
                interlist = interventionDao.GetAssociatedIntervention_ForManager(manager_userid);
                
                foreach (var inter in interlist)
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
    }
}