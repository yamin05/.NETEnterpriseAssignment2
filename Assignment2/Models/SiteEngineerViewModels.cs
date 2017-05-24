﻿using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class CreateNewClientViewModel
    {
        [Required]
        [Display(Name = "Client Name")]
        [RegularExpression("[a-zA-Z ]{1,}", ErrorMessage = "You can only put alphabets in words for name")]
        public string clientName { get; set; }

        [Required]
        [Display(Name = "Client Location")]
        public string clientLocation { get; set; }

        [Required]
        [Display(Name = "Client District")]
        public string clientDistrict { get; set; }
    }

    public class GetAllClientsViewModel
    {
        [Display(Name = "Client ID")]
        public int clientID { get; set; }

        [Display(Name = "Client Name")]
        public string clientName { get; set; }

        [Display(Name = "Client Location")]
        public string clientLocation { get; set; }

        [Display(Name = "Client District")]
        public string clientDistrict { get; set; }
    }
}