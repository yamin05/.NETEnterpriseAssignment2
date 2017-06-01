using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class TotalCostsByEngineerModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public decimal TotalHours { get; set; }

        public decimal TotalCosts { get; set; }

    }
}