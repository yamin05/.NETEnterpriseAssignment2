using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web;
using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class CreateInterventionHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;
        public IList<InterventionType> intType { get; private set; }

        public CreateInterventionHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }

        public CreateInterventionHelper()
        {
     
        }

        public IList<InterventionType> GetInterventionTypes ()
        {
            var repos = new InterventionTypeRepository(context);
            var list = repos.GetAll();
            intType = list;
            return list;
        }

        //get the client name list
        public IList<Clients> GetClientNames()
        {
            var repos = new ClientRepository(context);
            var userRepo = new UserRepository(context);
            var user = userRepo.GetAllForUser(Utils.getInstance.GetCurrentUserId());
            var list = repos.GetAllClientsForUser(Utils.getInstance.GetCurrentUserId(), user.District);
            return list;
        }

        //create an intervention
        public void CreateIntervention (int interventionTypeId, int clientId, string interventionHour, string interventionCost)
        {
            var repos = new InterventionsRepository(context);
            Interventions intervention = new Interventions();
            intervention.UserId = HttpContext.Current.User.Identity.GetUserId();
            intervention.InterventionTypeId = interventionTypeId;
            intervention.ClientId = clientId;
            intervention.CreateDate = DateTime.Now;
            intervention.InterventionHours = Convert.ToDecimal(interventionHour);
            intervention.InterventionCost = Convert.ToDecimal(interventionCost);
            intervention.Status = (validateUserForStatus(intervention.InterventionHours, intervention.InterventionCost)) ? 
                (int) Status.Approved : (int)Status.Proposed;
            intervention.Operator = "";
            try
            {
                repos.Insert(intervention);
            }
            catch (Exception)
            {
                throw new FailedToCreateRecordException();
            }
        }

        //validate the status of user
        private bool validateUserForStatus(decimal hours, decimal cost)
        {
            var userRepo = new UserRepository(context);
            var currentUser = userRepo.GetAllForUser(Utils.getInstance.GetCurrentUserId());
            if (currentUser.MaximumHours >= hours && currentUser.MaximumCost >= cost)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}