using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Dashboard
    {
        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email length should not be more than 150")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required", AllowEmptyStrings = false)]
        [StringLength(500, ErrorMessage = "Message length should not be more than 500")]
        public string Message { get; set; }

        public string AlertMessage { get; set; }

        public Login Login { get; set; }
    }
}