using Assignment2.Models.Database_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// This is a view model for the Interventions connected to Intervention model
/// </summary>
namespace Assignment2.Models
{
    /// <summary>
    /// Annotations and validation for create Intervention page
    /// </summary>
    public class CreateNewInterventionViewModel
    {
        [Required]
        [Display(Name = "Client ID")]
        public int clientId { get; set; }

        [Display(Name = "Client Name")]
        public string clientName { get; set; }

        [Required]
        [Display(Name = "Intervention Type")]
        public int interventionTypeId { get; set; }

        [Required]
        [Display(Name = "Intervention Cost")]
        public decimal interventionCost { get; set; }

        [Required]
        [Display(Name = "Intervention Hours")]
        public decimal interventionHours { get; set; }

        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string comments { get; set; }

    }

    /// <summary>
    /// Annotations for View Interventions page - Index
    /// </summary>
    public class ListInterventionViewModel {

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
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        [Display(Name = "Intervention Id")]
        public int InterventionId { get; set; }
        [Display(Name = "Modified Date")]
        public DateTime? ModifyDate { get; set; }
        [Display(Name = "Life in %")]
        [Range(typeof(int), "0", "100", ErrorMessage ="Life can be only between 0 - 100 %")]
        public int? Condition { get; set; }
        [Display(Name = "Comments")]
        [DataType(DataType.MultilineText)]
        public string comments { get; set; }
    }
}