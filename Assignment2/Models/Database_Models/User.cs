﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.Database_Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }     
        public decimal MaximumHours { get; set; }
        public decimal MaximumCost { get; set; }
        public string District { get; set; }
        public virtual ICollection<Intervention> UpdatedBy { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}