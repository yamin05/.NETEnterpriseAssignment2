using System.Collections.Generic;
using Assignment2.Models;
using Assignment2.Models.Database_Models;

namespace Assignment2.Helpers
{
    public interface IInterventionHelper
    {
        void CreateIntervention(int clientId, int interventionTypeId, decimal interventionCost, decimal interventionHours);
        ListInterventionViewModel GetIntervention(int? userid);
        InterventionType GetInterventionTypeData(int interventionTypeId);
        User GetManagerData(string userId);
        Dictionary<string, string> GetPossibleStatusUpdateForIntervention(string status);
        Dictionary<string, string> GetPossibleStatusUpdateForInterventionForSiteEngineer(string status);
        User GetSiteEngineerData(string userId);
        IList<ListInterventionViewModel> ListInterventions();
        IList<ListInterventionViewModel> ListOfClientsInterventions(int? id);
        IList<Intervention> ListofProposedIntervention();
        IList<ListInterventionViewModel> ListOfProposedInterventionsForManager();
        string ValidateInterventionStatus(decimal requiredHours, decimal requiredCost, User user);
        IList<Intervention> ValidateProposedInterventions(User manager, IList<Intervention> InterList);
    }
}