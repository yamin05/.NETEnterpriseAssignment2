using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class TotalCostsByEngineerModel
    {
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Total Hours")]
        public decimal TotalHours { get; set; }
        [Display(Name = "Total Costs")]
        public decimal TotalCosts { get; set; }

    }
}