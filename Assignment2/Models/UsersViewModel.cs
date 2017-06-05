using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        [Display(Name = "Maximum Cost")]
        public decimal MaximumCost { get; set; }
        [Display(Name = "Maximum Hours")]
        public decimal MaximumHours { get; set; }

        public string District { get; set; }
    }
}