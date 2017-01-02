using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Dashboard
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public Login Login { get; set; }
    }
}