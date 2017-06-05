using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;

namespace Assignment2.Helpers
{
    public class ManagerHelper
    {
        private IDao dao;

        public ManagerHelper()
        {
            dao = new Dao();
        }

        public IList<ListInterventionViewModel> GetListOfProposedInterventions()
        {
            try
            {
                IList<ListInterventionViewModel> ViewList = new List<ListInterventionViewModel>();
                var manager = dao.GetUser(Utils.getInstance.GetCurrentUserId());
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

        private IList<Intervention> ListofProposedIntervention()
        {
            IList<Intervention> ProposedInterList = new List<Intervention>();
            ProposedInterList = dao.GetInterventions(Status.PROPOSED);
            return ProposedInterList;
        }

        private IList<Intervention> ValidateProposedInterventions(User manager, IList<Intervention> InterList)
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

        public ListInterventionViewModel GetIntervention(int interventionId)
        {
            try
            {
                Intervention Inter = new Intervention();
                Inter = dao.GetIntervention(interventionId);
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

        public Intervention UpdateInterventionStatus(int interventionId, string newStatus)
        {
            try
            {
                var intervention = dao.GetIntervention(interventionId);
                if (intervention.Status.Equals(newStatus))
                {
                    throw new CannotEditStatusException();
                }
                var manager = dao.GetUser(Utils.getInstance.GetCurrentUserId());
                var inter = dao.UpdateIntervention(interventionId, manager, intervention.Status, newStatus);
                var mailHelper = new MailHelper();
                mailHelper.SendMail(manager.UserId, intervention.CreatedByUserId, intervention, inter.Status);
                return inter;
            }
            catch (Exception ex)
            {
                if (ex is CannotEditStatusException)
                {
                    throw;
                }
                else
                {
                    throw new FailedToUpdateRecordException();
                }
            }
        }
    }
}