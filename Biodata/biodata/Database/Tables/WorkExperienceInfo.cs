using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class WorkExperienceInfo
    {
        [Key]
        public int Id { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string TotalExperience { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}