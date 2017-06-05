using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Models
{
    public class CostsByDistrictModel
    {
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        public decimal Hours { get; set; }

        public decimal Costs { get; set; }

    }
}