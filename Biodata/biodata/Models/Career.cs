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
        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Company is required")]
        public string Company { get; set; }

        public string Location { get; set; }

        public string WorkingFrom { get; set; }

        public bool YesWorkExperience { get; set; }
        
        public string AnnualIncomeText { get; set; }

        public IEnumerable AnnualIncomeList { get; set; }
    }
}