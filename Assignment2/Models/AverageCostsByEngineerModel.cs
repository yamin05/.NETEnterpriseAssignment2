using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class AverageCostsByEngineerModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public decimal AverageHours { get; set; }

        public decimal AverageCosts { get; set; }


    }
}