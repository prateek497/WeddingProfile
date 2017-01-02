using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "Please enter vaild email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}