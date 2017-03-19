using System;
using System.Collections;
using System.Collections.Generic;
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

        public string College { get; set; }

        public string University { get; set; }

        public string Degree { get; set; }

        public string Year { get; set; }

        public IEnumerable EducationList { get; set; }

        public string EducationQualText { get; set; }
    }
}