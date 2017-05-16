using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Database_Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string UserName { get; set; }
        
        public decimal MaximumHours { get; set; }

        public decimal MaximumCost { get; set; }

        public string District { get; set; }
    }
}