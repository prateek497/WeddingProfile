using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email length should not be more than 150")]
        public string FormatEmail { get; set; }

        public string AlertMessage { get; set; }
    }
}