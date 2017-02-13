using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class FamilyInfo
    {
        [Key]
        public int Id { get; set; }
        public string Relationship { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string Joblocation { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}