using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class MonthlyCostsForDistrictModel
    {

        public string Month { get; set; }
        public string District { get; set; }
        [Display(Name = "Monthly Hours")]
        public decimal MonthlyHours { get; set; }
        [Display(Name = "Monthly Costs")]
        public decimal MonthlyCosts { get; set; }

    }
}