using System.Collections.Generic;
using Assignment2.Models;
using Assignment2.Models.Database_Models;

namespace Assignment2.Helpers
{
    public interface IManagerHelper
    {
        ListInterventionViewModel GetIntervention(int interventionId);
        IList<ListInterventionViewModel> GetListOfProposedInterventions();
        IList<string> GetPossibleStatusUpdateForIntervention(string status);
        Intervention UpdateInterventionStatus(int interventionId, string newStatus);
    }
}