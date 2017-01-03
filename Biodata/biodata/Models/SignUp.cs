using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace biodata.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email length should not be more than 150")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Password should contain atleast 6 charactor")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Comfirm password is required", AllowEmptyStrings = false)]
        [Compare("Password", ErrorMessage = "Password should be same")]
        public string ConfirmPassword { get; set; }
    }
}