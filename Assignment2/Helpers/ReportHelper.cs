using Assignment2.Data_Access_Layer;
using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Helpers
{
    public class ReportHelper
    {
        private ReportDao reportDao;
        public ReportHelper()
        {
            reportDao = new ReportDao();
        }

        public IList<TotalCostsByEngineerModel> TotalCostsByEngineerView()
        {
            IList<TotalCostsByEngineerModel> viewList = new List<TotalCostsByEngineerModel>();
            viewList = reportDao.TotalCostsByEngineerView();
            return viewList;
        }

        public IList<AverageCostsByEngineerModel> AverageCostsByEngineerView()
        {
            IList<AverageCostsByEngineerModel> viewList = new List<AverageCostsByEngineerModel>();
            viewList = reportDao.AverageCostsByEngineerView();
            return viewList;
        }
    }
}