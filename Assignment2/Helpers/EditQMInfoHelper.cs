using System;
using System.Collections.Generic;
using WebApplication2.Exceptions;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Helpers
{
    public class EditQMInfoHelper
    {
        private DbConnectionFactory factory;
        private DbContext context;
        private InterventionUpdate intUpdate = new InterventionUpdate();
        private InterventionUpdateRepository intUpdateRepository;

        public EditQMInfoHelper(string connectionString)
        {
            factory = new DbConnectionFactory(connectionString);
            context = new DbContext(factory);
        }

        public EditQMInfoHelper()
        {

        }

        
        public IList<InterventionUpdate> GetAllPreviousUpdates(string intId)
        {
            intUpdateRepository = new InterventionUpdateRepository(context);
            return intUpdateRepository.GetAllInterventionUpdateWithInterventionId(Convert.ToInt32(intId));
        }

        public void updateQMInfo(string intId, string comments, string conditionText)
        {
            intUpdate.InterventionId = Convert.ToInt32(intId);
            intUpdate.Condition = Convert.ToInt32(conditionText);
            intUpdate.ModifyDate = DateTime.Now;
            intUpdate.UserId = Utils.getInstance.GetCurrentUserId();
            intUpdate.InterventionComments = comments;
            intUpdateRepository = new InterventionUpdateRepository(context);
            validateUser();
            validateCondition();
            validatePreviousUpdate();
            intUpdateRepository.Insert(intUpdate);
        }

        //validate the site engineer
        private bool validateUser()
        {
            var role = Utils.getInstance.GetCurrentUserRole();
            if(role.Equals(Roles.SiteEngineer.ToString()))
            { 
                return isInterventionClientInSameDistrict();
            } else
            {
                throw new EditQMInfoPermissionException();
            }
        }

        //check the intervention and client are in same district or not
        private bool isInterventionClientInSameDistrict()
        {
            var repo = new InterventionsRepository(context);
            var result = repo.GetInterventionForClientInSameDistrict(intUpdate.InterventionId, Utils.getInstance.GetCurrentUserId());
            if (!Utils.getInstance.isNullOrEmpty(result))
            {
                return true;
            } else
            {
                throw new EditStatusPermissionException();
            }
        }

        //validate the update of intervention
        private bool validatePreviousUpdate()
        {
            var lastIntUpdate = intUpdateRepository.GetLastInterventionUpdateWithInterventionId(intUpdate.InterventionId);
            if (Utils.getInstance.isNullOrEmpty(lastIntUpdate))
            {
                return true;
            } else
            {
                if (lastIntUpdate.Condition > intUpdate.Condition)
                {
                    throw new InvalidConditionUpdateException();
                }
                return true;
            }
        }

        private bool validateCondition()
        {
            if (intUpdate.Condition > 100)
            {
                throw new InvalidConditionLimitException();
            }
            else
            {
                return true;
            }
        }
    }
}