using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class EducationInfo
    {
        [Key]
        public int Id { get; set; }
        public string HigherSecondaryYear { get; set; }
        public string HigherSecondarySchool { get; set; }
        public string GraduateDegree { get; set; }
        public string GraduatedYear { get; set; }
        public string GraduateCollege { get; set; }
        public string GraduateUniversity { get; set; }
        public string PostGraduateDegree { get; set; }
        public string PostGraduateYear { get; set; }
        public string PostGraduateCollege { get; set; }
        public string PostGraduateUniversity { get; set; }
        public string DoctarateDegree { get; set; }
        public string DoctarateYear { get; set; }
        public string DoctarateCollege { get; set; }
        public string DoctarateUniversity { get; set; }
        public string AnnualIncome { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}