using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Data_Access_Layer
{
    public interface IReportDao
    {
        IList<TotalCostsByEngineerModel> TotalCostsByEngineerView();
        IList<AverageCostsByEngineerModel> AverageCostsByEngineerView();
        IList<CostsByDistrictModel> CostsByDistrictView();
        IList<MonthlyCostsForDistrictModel> MonthlyCostsForDistrictView(string district);
    }
}