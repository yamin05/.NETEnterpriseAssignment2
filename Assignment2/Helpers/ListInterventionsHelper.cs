using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.Exceptions;
namespace WebApplication2.Helpers
{
    public class ListInterventionsHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;

        public ListInterventionsHelper()
        {
            

        }
        public ListInterventionsHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);

        }

        //get the manager's max cost and maxhour
        private IList<string> Get_District_MaxCost_MaxHour_ForManager(string userId)
        {
            try
            {
                var repos = new UserRepository(context);
                var row = repos.GetAllForUser(userId);
                List<string> list = new List<string>();
                list.Add(row.District.ToString());
                list.Add(row.MaximumCost.ToString());
                list.Add(row.MaximumHours.ToString());
                return list;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }

        //get the list of intervention of manager
        public IList<ListInterventionForManager> GetInterventions(string userid)
        {
            try
            {
                var repos = new ListInterventionForManagerRepository(context);
                var rows = repos.GetAllProposedInterventiond();
                List<ListInterventionForManager> list = new List<ListInterventionForManager>();
                foreach (var row in rows)
                {
                    list.Add(row);

                }
                return list;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
         }

        //Gets the list of all intervention associated with a site engineer
        public IList<ListInterventions> GetInterventionsForUser()
        {
            var repos = new ListInterventionsRepository(context);
            var list = repos.GetAllInterventionsForUser(Utils.getInstance.GetCurrentUserId());
            return list;
        }

        //Gets the list of all intervention associated with a client
        public IList<ListInterventions> GetInterventionsForClient(string clientid)
        {
            try
            {
                var repos = new ListInterventionsRepository(context);
                var list = repos.GetAllInterventionsForClient(Utils.getInstance.GetCurrentUserId(), Convert.ToInt32(clientid));
                return list;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }
        //Gets the list of all intervention associated with a client in the same district
        public IList<ListInterventions> GetInterventionsForClientInSameDistrict(string clientid)
        {
            try
            {
                var repos = new ListInterventionsRepository(context);
                var list = repos.GetAllInterventionsForClientInSameDistrict(Convert.ToInt32(clientid));
                return list;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }
        //Returns a list of status based current status, used for populating a drop down on change state
        public Dictionary<string, int> GetPossibleStatusUpdateForIntervention(string status)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            if (Status.Proposed.ToString().Equals(status))
            {
                list.Add(Status.Approved.ToString(), (int)Status.Approved);
                //list.Add(Status.Completed.ToString(), (int)Status.Completed);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            else if (Status.Approved.ToString().Equals(status))
            {
                list.Add(Status.Completed.ToString(), (int)Status.Completed);
                list.Add(Status.Cancelled.ToString(), (int)Status.Cancelled);
            }
            return list;
        }
        //Gets a list of all proposed interventions from the database
        public IList<ListInterventionForManager> ListOfPropInterventions(string userid, int InterventionId)
        {
            try
            {
                List<ListInterventionForManager> interlist = new List<ListInterventionForManager>();
                ListInterventionForManagerRepository repo = new ListInterventionForManagerRepository(context);
                interlist = repo.GetAllInterventionByInterventionId(InterventionId).ToList();
                return interlist;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }
        //used for cancelling an intervention by manager
        public IList<InterventionsRepository> CancelIntervention(int interventionId)
        {
            try
            {
                InterventionsRepository repo = new InterventionsRepository(context);
                List<InterventionsRepository> interlist = new List<InterventionsRepository>();
                repo.Delete(interventionId);
                return interlist;

            }
            catch
            {
                throw new FailedToUpdateRecordException();
            }
        }
        //used for approving an intervention by manager
        public IList<InterventionsRepository> ApproveIntervention(int InterventionId, int OldStatus, int NewStatus, string Userid)
        {
            try
            {
                InterventionsRepository repo = new InterventionsRepository(context);
                List<InterventionsRepository> interlist = new List<InterventionsRepository>();
                repo.Update_Intervention_Status(InterventionId, OldStatus, NewStatus, Userid);
                return interlist;
            }
            catch
            {
                throw new FailedToUpdateRecordException();
            }
        }
        public IList<ListInterventionForManager> ListOfProposedInterventions(string userid)
        {
            try
            {
                List<ListInterventionForManager> interlist = new List<ListInterventionForManager>();
                interlist = GetInterventions(userid).ToList();
                var ManageruserId = HttpContext.Current.User.Identity.GetUserId();
                List<ListInterventionForManager> proposedinterlist = new List<ListInterventionForManager>();
                var manager = GetManagerInfo(ManageruserId);
                proposedinterlist = ValidateProposedInterventions(manager, interlist).ToList();
                return proposedinterlist;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }

        }
        public IList<ListInterventionForManager> ListOfAssociatedIntrevention(string userid)
        {
            try
            {
                List<ListInterventionForManager> interlist = new List<ListInterventionForManager>();
                ListInterventionForManagerRepository repo = new ListInterventionForManagerRepository(context);
                List<ListInterventionForManager> associatedlist = new List<ListInterventionForManager>();
                associatedlist = repo.GetAllInterventionAssociatedWithManager(userid);
                return associatedlist;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }
        }
        //get the information of manager: cost and hours
        public Users GetManagerInfo(string userid)
        {
            try
            {
                List<string> ManagerInfo = new List<string>();
                ManagerInfo = Get_District_MaxCost_MaxHour_ForManager(userid).ToList();
                string Dis = ManagerInfo.ElementAt(0);
                string maxihcost = ManagerInfo.ElementAt(1);
                string maxihour = ManagerInfo.ElementAt(2);
                Users manager = new Users();
                manager.District = Convert.ToInt32(Dis);
                manager.MaximumCost = Convert.ToDecimal(maxihcost);
                manager.MaximumHours = Convert.ToDecimal(maxihour);
                return manager;
            }
            catch
            {
                throw new FaliedToRetriveRecordException();
            }

        }

        //validate the proposed intervention
        public IList<ListInterventionForManager> ValidateProposedInterventions(Users manager, List<ListInterventionForManager> InterList)
        {
            List<ListInterventionForManager> ProposedList = new List<ListInterventionForManager>();
            for (int i = 0; i <= InterList.Count - 1; i++)
            {
                {
                    if (manager.MaximumHours >= InterList[i].InterventionHours && manager.MaximumCost >= InterList[i].InterventionCost && manager.District == InterList[i].District)
                    {
                        ProposedList.Add(InterList[i]);
                        
                    }
                }
            }
            return ProposedList;
        }


    }
}



    
    

