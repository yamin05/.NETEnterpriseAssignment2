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
        [ForeignKey("CreatedBy")]
        public string CreatedByUserId { get; set; }
        [ForeignKey("ApprovedBy")]
        public string ApprovedByUserId { get; set; }
        [ForeignKey("InterTypeId")]
        public int InterventionTypeId { get; set; }
        public Client ClientId { get; set; }
        public decimal InterventionCost { get; set; }
        public decimal InterventionHours { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public int? Condition { get; set; }
        public DateTime? ModifyDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual ICollection<User> UpdatedBy { get; set; }
        public virtual InterventionType InterTypeId { get; set; }
    }
}