using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Data_Access_Layer
{
    public class ReportDao
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
                                select new { UserId=tb1.Id, RoleName=tb3.Name.Replace("_"," "), UserName=tb1.UserName }).ToList();

                var totalCostsByEngineer = (from tb1 in context.Interventions
                                            group tb1 by tb1.CreatedByUserId into tb2
                                            select new
                                            {
                                                UserId= tb2.Key,
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
                                                     TotalHours =(tb3 == null)? 0 : tb3.TotalHours,
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
    }
}