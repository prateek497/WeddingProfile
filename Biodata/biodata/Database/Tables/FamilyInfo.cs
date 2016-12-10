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
        public string FatherName { get; set; }
        public string FatherCity { get; set; }
        public string FatherState { get; set; }
        public string FatherDesignation { get; set; }
        public string FatherCompany { get; set; }
        public string MotherName { get; set; }
        public string MotherCity { get; set; }
        public string MotherState { get; set; }
        public string MotherDesignation { get; set; }
        public string MotherCompany { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}