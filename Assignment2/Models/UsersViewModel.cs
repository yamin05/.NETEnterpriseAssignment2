using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

        public decimal MaximumCost { get; set; }

        public decimal MaximumHours { get; set; }

        public string District { get; set; }
    }
}