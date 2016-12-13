using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class ContactInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameRelation { get; set; }
        public string ContactNumber { get; set; }
        public string ContactNumberRelation { get; set; }
        public string Email { get; set; }
        public string EmailRelation { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}