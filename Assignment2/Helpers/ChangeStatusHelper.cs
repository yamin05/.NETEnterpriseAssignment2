using System;
using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class ChangeStatusHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;
        private Interventions intervention = new Interventions();

        public ChangeStatusHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }


        public ChangeStatusHelper()
        {

        }

        //Change the status of a intervention
        public void ChangeStatus(string intId, string oldStatusText, string newStatusNumber)
        {
            intervention.InterventionId = Convert.ToInt32(intId);
            var intRepo = new InterventionsRepository(context);
            intervention = intRepo.GetInterventionWithInterventionId(intervention.InterventionId);
            var oldStatus = (int)Enum.Parse(typeof(Status), oldStatusText);
            var newStatus = Convert.ToInt32(newStatusNumber);
            validateUserRole();
            validateUserDistrict();
            validateHoursCost();
            validateOldStatus(oldStatus);
            var repo = new InterventionsRepository(context);
            var row = repo.UpdateInterventionStatus(intervention.InterventionId, oldStatus, newStatus);
        }

        //validate the current role of user
        private bool validateUserRole()
        {
            var role = Utils.getInstance.GetCurrentUserRole();
            if(role.Equals(Roles.SiteEngineer.ToString()))
            {
                return true;
            } else if (role.Equals(Roles.Manager.ToString()))
            {
                return isInterventionClientInSameDistrict();
            } else
            {
                throw new EditStatusPermissionException();
            }
        }

        //validate the district of user for accessing edit premissions
        private bool validateUserDistrict()
        {
            var repo = new InterventionsRepository(context);
            var checkedIntervention = repo.GetInterventionForClientInSameDistrict(intervention.InterventionId, Utils.getInstance.GetCurrentUserId());
            if (!Utils.getInstance.isNullOrEmpty(checkedIntervention))
            {
                return true;
            }
            else
            {
                throw new EditStatusPermissionException();
            }
        }

        //check the intervention and client are in some district
        private bool isInterventionClientInSameDistrict()
        {
            var repo = new InterventionsRepository(context);
            var result = repo.GetInterventionForClientInSameDistrict(intervention.InterventionId, Utils.getInstance.GetCurrentUserId());
            if (!Utils.getInstance.isNullOrEmpty(result))
            {
                return true;
            } else
            {
                throw new EditStatusPermissionException();
            }
        }

        //validate the hours and cost required for the intervention with the max hours and max cost of Site Engineer
        private bool validateHoursCost()
        {
            var userRepo = new UserRepository(context);
            var currentUser = userRepo.GetAllForUser(Utils.getInstance.GetCurrentUserId());
            var intTypeRepo = new InterventionTypeRepository(context);
            var intType = intTypeRepo.GetInterventionTypeWithId(intervention.InterventionTypeId);
            if (currentUser.MaximumHours >= intervention.InterventionHours && currentUser.MaximumHours >= intType.InterventionTypeHours &&
                currentUser.MaximumCost >= intervention.InterventionCost && currentUser.MaximumCost >= intType.InterventionTypeCost)
            {
                return true;
            } else
            {
                throw new EditStatusPermissionException();
            }
        }

        //validate the status of intervention
        private bool validateOldStatus(int oldStatus)
        {
            if (oldStatus == (int)Status.Cancelled || oldStatus == (int)Status.Completed)
            {
                throw new CannotEditStatusException();
            } else if (oldStatus == (int)Status.Approved )
            {
                if (Utils.getInstance.GetCurrentUserRole().Equals(Roles.SiteEngineer.ToString()))
                {
                    return true;
                } else
                {
                    throw new EditStatusPermissionException();
                }
            }
            else
            {
                return true;
            }
        }
    }
}