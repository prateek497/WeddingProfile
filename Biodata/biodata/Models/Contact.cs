using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Contact
    {
        //[Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        public string Name { get; set; }

        public List<string> RelationshipList { get; set; }

        //[Required(ErrorMessage = "Relationship is required")]
        public string RelationshipText { get; set; }

        //[Required(ErrorMessage = "Phone is required", AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }

        //[Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}