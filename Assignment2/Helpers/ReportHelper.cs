using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Helpers
{
    public  class ReportHelper : IReportHelper
    {
        private IReportDao reportDao;
        public ReportHelper()
        {
            reportDao = new ReportDao();
        }

        /// <summary>
        /// This method is for view the total costs by engineer view
        /// </summary>
        public IList<TotalCostsByEngineerModel> TotalCostsByEngineerView()
        {
            IList<TotalCostsByEngineerModel> viewList = new List<TotalCostsByEngineerModel>();
            viewList = reportDao.TotalCostsByEngineerView();
            return viewList;
        }

        /// <summary>
        /// This method is for view the average costs by engineer view
        /// </summary>
        public IList<AverageCostsByEngineerModel> AverageCostsByEngineerView()
        {
            IList<AverageCostsByEngineerModel> viewList = new List<AverageCostsByEngineerModel>();
            viewList = reportDao.AverageCostsByEngineerView();
            return viewList;
        }

        /// <summary>
        /// This method is for view the costs by district view
        /// </summary>
        public IList<CostsByDistrictModel> CostsByDistrictView()
        {
            IList<CostsByDistrictModel> viewList = new List<CostsByDistrictModel>();        
            viewList = reportDao.CostsByDistrictView();
            return viewList;
        }

        /// <summary>
        /// This method is for view the monthly costs for district view with district
        /// </summary>
        public IList<MonthlyCostsForDistrictModel> MonthlyCostsForDistrictView(string district)
        {
            IList<MonthlyCostsForDistrictModel> viewList = new List<MonthlyCostsForDistrictModel>();
            viewList = reportDao.MonthlyCostsForDistrictView(district);
            return viewList;
        }
    }
}