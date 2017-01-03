using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class SignIn
    {
        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email length should not be more than 150")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Password should contain atleast 6 charactor")]
        public string Password { get; set; }
    }
}