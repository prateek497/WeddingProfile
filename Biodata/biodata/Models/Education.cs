using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{

    public class Educations
    {
        public EducationFields Education { get; set; }

        public List<EducationFields> EducationFieldses { get; set; }
    }

    public class EducationFields
    {
        public int Id { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "College cannot be longer than 40 characters.")]
        public string College { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "University cannot be longer than 40 characters.")]
        public string University { get; set; }

        [Required(ErrorMessage = "Degree is required", AllowEmptyStrings = false)]
        [StringLength(40, MinimumLength = 1, ErrorMessage = "Degree cannot be longer than 40 characters.")]
        public string Degree { get; set; }

        [StringLength(4, MinimumLength = 1, ErrorMessage = "Year cannot be longer than 4 characters.")]
        public string Year { get; set; }

        public List<string> EducationList { get; set; }

        [Required(ErrorMessage = "Qualification is required", AllowEmptyStrings = false)]
        public string EducationQualText { get; set; }
    }
}