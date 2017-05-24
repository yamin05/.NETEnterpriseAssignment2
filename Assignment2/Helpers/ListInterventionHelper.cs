using Assignment2.Data_Access_Layer;
using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;
using Assignment2.Models;
using Microsoft.AspNet.Identity;
using System.Web;

namespace Assignment2.Helpers
{
    public class ListInterventionHelper
    {
        InterventionsDao InterventionDao = new InterventionsDao();
        Dao UDao = new Dao();
        public ListInterventionHelper()
        {

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

