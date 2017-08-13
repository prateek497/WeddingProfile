using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; }

        public List<string> RelationshipList { get; set; }

        [Required(ErrorMessage = "Relationship is required")]
        public string RelationshipText { get; set; }

        [Required(ErrorMessage = "Phone is required", AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [MaxLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [StringLength(40, ErrorMessage = "City cannot be longer than 40 characters.")]
        public string City { get; set; }

        [StringLength(40, ErrorMessage = "State cannot be longer than 40 characters.")]
        public string State { get; set; }
    }
}