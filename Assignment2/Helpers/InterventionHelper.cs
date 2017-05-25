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
    public class InterventionHelper
    {
        InterventionsDao InterventionDao = new InterventionsDao();
        private Dao UDao;
        private InterventionsDao interventionDao;
        public InterventionHelper()
        {
            UDao = new Dao();
            interventionDao = new InterventionsDao();
        }

        public void CreateIntervention(Client clientId, int interventionTypeId, decimal interventionCost, decimal interventionHours)
        {
            var intervention = new Intervention();
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

        public Intervention GetIntervention(int? userid)
        {
            Intervention Inter = new Intervention();
            Inter = InterventionDao.GetIntervention(userid);
            return Inter;
        }

        public IList<Intervention> ListofProposedIntervention()
        {
            IList<Intervention> ProposedInterList = new List<Intervention>();
            ProposedInterList = InterventionDao.GetInterventionsByStatus(Status.PROPOSED);
            return ProposedInterList;
        }

        public IList<Intervention> ValidateProposedInterventions(User manager, IList<Intervention> InterList)
        {
            IList<Intervention> ProposedList = new List<Intervention>();
            for (int i = 0; i <= InterList.Count - 1; i++)
            {
                {
                    if (manager.MaximumHours >= InterList[i].InterventionHours && manager.MaximumCost >= InterList[i].InterventionCost && manager.District == InterList[i].ClientId.ClientDistrict)
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
        public IList<ListInterventionViewModel> ListOfProposedInterventions()
        {
            try
            {
                IList<Intervention> interlist = new List<Intervention>();
                
                var ManageruserId = HttpContext.Current.User.Identity.GetUserId();
                IList<Intervention> proposedinterlist = new List<Intervention>();
                IList<ListInterventionViewModel> ViewList=new List<ListInterventionViewModel>();
                var manager = GetManagerData(ManageruserId);
                proposedinterlist = ValidateProposedInterventions(manager, interlist);
                foreach(var inter in proposedinterlist)
                {
                    ListInterventionViewModel ViewIntervention = new ListInterventionViewModel();
                    ViewIntervention.ClientDistrict = inter.ClientId.ClientDistrict;
                    ViewIntervention.ClientName = inter.ClientId.ClientName;
                    ViewIntervention.InterventionTypeName = inter.InterTypeId.InterventionTypeName;
                    ViewIntervention.InterventionCost = inter.InterventionCost;
                    ViewIntervention.InterventionHours = inter.InterventionHours;
                    ViewIntervention.CreateDate = inter.CreateDate;
                    ViewIntervention.InterventionId = inter.InterventionId;
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

