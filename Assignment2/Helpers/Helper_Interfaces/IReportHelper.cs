using System.Collections.Generic;
using Assignment2.Models;

namespace Assignment2.Helpers
{
    public interface IReportHelper
    {
        IList<AverageCostsByEngineerModel> AverageCostsByEngineerView();
        IList<CostsByDistrictModel> CostsByDistrictView();
        IList<MonthlyCostsForDistrictModel> MonthlyCostsForDistrictView(string district);
        IList<TotalCostsByEngineerModel> TotalCostsByEngineerView();
    }
}