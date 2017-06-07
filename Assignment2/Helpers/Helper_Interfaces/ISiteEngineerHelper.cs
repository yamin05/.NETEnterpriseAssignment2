using System.Collections.Generic;
using Assignment2.Models;
using Assignment2.Models.Database_Models;

namespace Assignment2.Helpers
{
    public interface ISiteEngineerHelper
    {
        void CreateClient(string clientName, string clientLocation, string clientDistrict);
        void CreateIntervention(int clientId, int interventionTypeId, decimal interventionCost, decimal interventionHours);
        string GetDistrictForUser();
        IList<Client> ListCurrentClients();
        IList<ListInterventionViewModel> ListInterventions();
        IList<InterventionType> ListInterventionTypes();
        IList<ListClientsViewModel> ListOfClients();
        IList<ListClientsViewModel> ListOfClientsInDistrict();
        string ValidateInterventionStatus(int interventionTypeId, decimal requiredHours, decimal requiredCost);
    }
}