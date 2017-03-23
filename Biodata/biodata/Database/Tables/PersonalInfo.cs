using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class PersonalInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public DateTime DobTime { get; set; }
        public string CurrentCity { get; set; }
        public string Height { get; set; }
        public string Complexion { get; set; }
        public string MaritalStatus { get; set; }
        public string Diet { get; set; }
        public string Drink { get; set; }
        public string Smoke { get; set; }
        public string Hobbies { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Quora { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}