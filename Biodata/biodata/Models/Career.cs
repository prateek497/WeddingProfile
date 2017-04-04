using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Career
    {
        [Required(ErrorMessage = "Designation is required", AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Designation cannot be longer than 40 characters.")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Company is required", AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Company name cannot be longer than 40 characters.")]
        public string Company { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Location cannot be longer than 40 characters.")]
        public string Location { get; set; }

        public string WorkingFrom { get; set; }

        public bool YesWorkExperience { get; set; }
        
        public string AnnualIncomeText { get; set; }

        public IEnumerable AnnualIncomeList { get; set; }
    }
}