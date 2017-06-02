using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class MonthlyCostsForDistrictModel
    {

        public string Month { get; set; }

        public string District { get; set; }

        public decimal MonthlyHours { get; set; }

        public decimal MonthlyCosts { get; set; }

    }
}