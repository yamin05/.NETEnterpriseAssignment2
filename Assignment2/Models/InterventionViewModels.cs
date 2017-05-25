using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class CreateNewInterventionViewModel
    {
        [Required]
        [Display(Name = "Client Name")]
        public Client clientId { get; set; }

        [Required]
        [Display(Name = "Intervention Type")]
        public int interventionTypeId { get; set; }

        [Required]
        [Display(Name = "Intervention Cost")]
        public decimal interventionCost { get; set; }

        [Required]
        [Display(Name = "Intervention Hours")]
        public decimal interventionHours { get; set; }

    }
}