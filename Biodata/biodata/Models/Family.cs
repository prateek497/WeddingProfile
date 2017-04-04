using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Families
    {
        public List<Family> FamilyList { get; set; }

        public Family FamilyMember { get; set; }
    }

    public class Family
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required", AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "City cannot be longer than 40 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required", AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "State cannot be longer than 40 characters.")]
        public string State { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Designation cannot be longer than 40 characters.")]
        public string Designation { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Company Name cannot be longer than 40 characters.")]
        public string CompanyName { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Job location cannot be longer than 40 characters.")]
        public string JobLocation { get; set; }

        [Required(ErrorMessage = "Relationship is required", AllowEmptyStrings = false)]
        public string RelationshipText { get; set; }

        public List<string> RelationshipList { get; set; }
    }
}