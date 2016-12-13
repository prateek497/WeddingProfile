using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class SocialMedia
    {
        [Key]
        public int Id { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Quora { get; set; }
        public int? UserId { get; set; }

        public  User User { get; set; }
    }
}