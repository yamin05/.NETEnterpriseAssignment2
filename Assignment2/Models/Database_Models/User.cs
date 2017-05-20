using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models.Database_Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }     
        public decimal MaximumHours { get; set; }
        public decimal MaximumCost { get; set; }
        public string District { get; set; }
        public virtual ICollection<Intervention> Interventions { get; set; }
    }
}