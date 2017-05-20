using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Database_Models
{
    [Serializable]
    public class InterventionType
    {
        [Key]
        public int InterventionTypeId { get; set; }
        public string InterventionTypeName { get; set; }
        public decimal InterventionTypeHours { get; set; }
        public decimal InterventionTypeCost { get; set; }
        public virtual ICollection<Intervention> Interventions { get; set; }
    }
}