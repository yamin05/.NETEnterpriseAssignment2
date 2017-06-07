using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models.Database_Models
{
    public class Intervention
    {
        [Key]
        public int InterventionId { get; set; }
        public string CreatedByUserId { get; set; }
        public string ApprovedByUserId { get; set; }
        public string LastUpdatedByUserId { get; set; }
        public int InterventionTypeId { get; set; }
        public int ClientId { get; set; }
        public decimal InterventionCost { get; set; }
        public decimal InterventionHours { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public String Comments { get; set; }
        public int? Condition { get; set; }
        public DateTime? ModifyDate { get; set; }
        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("ApprovedByUserId")]
        public virtual User ApprovedBy { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        [ForeignKey("LastUpdatedByUserId")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("InterventionTypeId")]
        public virtual InterventionType InterTypeId { get; set; }
    }
}