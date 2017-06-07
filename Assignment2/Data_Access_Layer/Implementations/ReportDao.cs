using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Assignment2.Data_Access_Layer
{
    public class ReportDao : IReportDao
    {
        private CustomDBContext context;
        private ApplicationDbContext context1;

        public IList<TotalCostsByEngineerModel> TotalCostsByEngineerView()
        {
            context = new CustomDBContext();
            context1 = new ApplicationDbContext();
            {
                var userList = (from tb1 in context1.Users
                                from tb2 in tb1.Roles
                                join tb3 in context1.Roles on tb2.RoleId equals tb3.Id
                                where tb3.Name == "Site_Engineer"
                                select new { UserId = tb1.Id, RoleName = tb3.Name.Replace("_", " "), UserName = tb1.UserName }).ToList();

                var totalCostsByEngineer = (from tb1 in context.Interventions
                                            where tb1.Status == Status.COMPLETED
                                            group tb1 by tb1.CreatedByUserId into tb2
                                            select new
                                            {
                                                UserId = tb2.Key,
                                                TotalHours = tb2.Sum(i => i.InterventionHours),
                                                TotalCosts = tb2.Sum(i => i.InterventionCost)
                                            }).ToList();

                IList<TotalCostsByEngineerModel> TotalCostsByEngineerView =
                                                (from tb1 in userList
                                                 join tb2 in totalCostsByEngineer
                                                 on tb1.UserId equals tb2.UserId into temp
                                                 from tb3 in temp.DefaultIfEmpty()
                                                 orderby tb1.UserName
                                                 select new TotalCostsByEngineerModel()
                                                 {
                                                     UserId = tb1.UserId,
                                                     RoleName = tb1.RoleName,
                                                     UserName = tb1.UserName,
                                                     TotalHours = (tb3 == null) ? 0 : tb3.TotalHours,
                                                     TotalCosts = (tb3 == null) ? 0 : tb3.TotalCosts
                                                 }).ToList();
                return TotalCostsByEngineerView;
            }
        }


        public IList<AverageCostsByEngineerModel> AverageCostsByEngineerView()
        {
            context = new CustomDBContext();
            context1 = new ApplicationDbContext();
            {
                var userList = (from tb1 in context1.Users
                                from tb2 in tb1.Roles
                                join tb3 in context1.Roles on tb2.RoleId equals tb3.Id
                                where tb3.Name == "Site_Engineer"
                                select new { UserId = tb1.Id, RoleName = tb3.Name.Replace("_", " "), UserName = tb1.UserName }).ToList();

                var totalCostsByEngineer = (from tb1 in context.Interventions
                                            where tb1.Status == Status.COMPLETED
                                            group tb1 by tb1.CreatedByUserId into tb2
                                            select new
                                            {
                                                UserId = tb2.Key,
                                                AverageHours = tb2.Average(i => i.InterventionHours),
                                                AverageCosts = tb2.Average(i => i.InterventionCost)
                                            }).ToList();

                IList<AverageCostsByEngineerModel> AverageCostsByEngineerView =
                                                (from tb1 in userList
                                                 join tb2 in totalCostsByEngineer
                                                 on tb1.UserId equals tb2.UserId into temp
                                                 from tb3 in temp.DefaultIfEmpty()
                                                 orderby tb1.UserName
                                                 select new AverageCostsByEngineerModel()
                                                 {
                                                     UserId = tb1.UserId,
                                                     RoleName = tb1.RoleName,
                                                     UserName = tb1.UserName,
                                                     AverageHours = (tb3 == null) ? 0 : tb3.AverageHours,
                                                     AverageCosts = (tb3 == null) ? 0 : tb3.AverageCosts
                                                 }).ToList();
                return AverageCostsByEngineerView;
            }
        }


        public IList<CostsByDistrictModel> CostsByDistrictView()
        {
            context = new CustomDBContext();
            {
                var districtList = new List<string>();

                districtList.Add(Districts.URBAN_INDONESIA);
                districtList.Add(Districts.RURAL_INDONESIA);
                districtList.Add(Districts.URBAN_PAPUA_NEW_GUINEA);
                districtList.Add(Districts.RURAL_PAPUA_NEW_GUINEA);
                districtList.Add(Districts.SYDNEY);
                districtList.Add(Districts.RURAL_NEW_SOUTH_WALES);

                var costsByDistrict = (from tb1 in context.Interventions.Include("Client")
                                       where tb1.Status == Status.COMPLETED
                                       group tb1 by tb1.Client.ClientDistrict into tb2
                                       select new
                                       {
                                           District = tb2.Key,
                                           Hours = tb2.Sum(i => i.InterventionHours),
                                           Costs = tb2.Sum(i => i.InterventionCost)
                                       }).ToList();

                IList<CostsByDistrictModel> CostsByDistrictView =
                                                (from tb1 in districtList
                                                 join tb2 in costsByDistrict
                                                 on tb1 equals tb2.District into temp
                                                 from tb3 in temp.DefaultIfEmpty()
                                                 select new CostsByDistrictModel()
                                                 {
                                                     DistrictName = tb1,
                                                     Hours = (tb3 == null) ? 0 : tb3.Hours,
                                                     Costs = (tb3 == null) ? 0 : tb3.Costs
                                                 }).ToList();

                IList<CostsByDistrictModel> CostsByDistrictWithTotalView1 = (from tb1 in CostsByDistrictView
                                                                    select tb1).ToList()
                                                                   .Union
                                                                    (from tb2 in CostsByDistrictView
                                                                          group tb2 by "" into tb2
                                                                          select new CostsByDistrictModel()
                                                                          {
                                                                              DistrictName = "Total",
                                                                              Hours = tb2.Sum(i => i.Hours),
                                                                              Costs = tb2.Sum(i => i.Costs)
                                                                          }).ToList();
                return CostsByDistrictWithTotalView1;
            }
        }

        public IList<MonthlyCostsForDistrictModel> MonthlyCostsForDistrictView(string district)
        {
            var yearList = new List<int> { 2017 };
            var monthList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var districtList = new List<string>();
            districtList.Add(Districts.URBAN_INDONESIA);
            districtList.Add(Districts.RURAL_INDONESIA);
            districtList.Add(Districts.URBAN_PAPUA_NEW_GUINEA);
            districtList.Add(Districts.RURAL_PAPUA_NEW_GUINEA);
            districtList.Add(Districts.SYDNEY);
            districtList.Add(Districts.RURAL_NEW_SOUTH_WALES);

            context = new CustomDBContext();

            var monthlyCosts = (from tb1 in context.Interventions.Include("Client")
                                where tb1.Status == Status.COMPLETED
                                group tb1 by new { tb1.Client.ClientDistrict, tb1.CreateDate.Year,tb1.CreateDate.Month } into tb2
                                select new
                                {
                                    District = tb2.Key.ClientDistrict,
                                    Year = tb2.Key.Year,
                                    Month = tb2.Key.Month,
                                    Hours = tb2.Sum(i => i.InterventionHours),
                                    Costs = tb2.Sum(i => i.InterventionCost)
                                }).ToList();

            IList<MonthlyCostsForDistrictModel> monthlyCostsForDistrict = (from tb1 in districtList
                                                                           from tb2 in monthList
                                                                           from tb22 in yearList
                                                                           join tb3 in monthlyCosts
                                                                           on new { district = tb1, month = tb2, year=tb22 } equals new { district = tb3.District, month = tb3.Month, year = tb3.Year } into temp
                                                                           from tb4 in temp.DefaultIfEmpty()
                                                                           where tb1 == district
                                                                           select new MonthlyCostsForDistrictModel()
                                                                           {
                                                                               District = tb1,
                                                                               Year = Convert.ToString(tb22),
                                                                               Month = new DateTime(2000, Convert.ToInt32(tb2), 1).ToString("MMMM", CultureInfo.GetCultureInfo("en-US")),
                                                                               MonthlyHours = (tb4 == null) ? 0 : tb4.Hours,
                                                                               MonthlyCosts = (tb4 == null) ? 0 : tb4.Costs
                                                                           }).ToList();
            return monthlyCostsForDistrict;
        }
    }
}