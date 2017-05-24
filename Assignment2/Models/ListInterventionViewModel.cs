using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class ListInterventionViewModel
    
    {
           
            [Display(Name = "Client District")]
            public string ClientDistrict { get; set; }
            [Display(Name = "Client Name")]
            public string ClientName { get; set; }
            [Display(Name = "Intervention Type Name")]
            public string InterventionTypeName { get; set; }
            [Display(Name = "Intervention Cost")]
            public decimal InterventionCost { get; set; }
            [Display(Name = "Intervention Hours")]
            public decimal InterventionHours { get; set; }
            [Display(Name = "Create Date")]
            public DateTime CreateDate { get; set; }
            public int Status { get; set; }
            [Display(Name = "Intervention Id")]
            public int InterventionId { get; set; }

        }
    }
