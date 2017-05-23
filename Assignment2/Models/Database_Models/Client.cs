using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models.Database_Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientLocation { get; set; }
        public string ClientDistrict { get; set; }
        [ForeignKey("CreatedBy")]
        public string CreatedByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Intervention> Interventions { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}