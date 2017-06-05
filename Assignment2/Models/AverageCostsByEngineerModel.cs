using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class AverageCostsByEngineerModel
    {
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Average Hours")]
        public decimal AverageHours { get; set; }
        [Display(Name = "Average Costs")]
        public decimal AverageCosts { get; set; }


    }
}