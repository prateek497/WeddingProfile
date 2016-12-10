using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class CulturalInfo
    {
        [Key]
        public int Id { get; set; }
        public string Religion { get; set; }
        public string MotherTongue { get; set; }
        public string Caste { get; set; }
        public string Gotra { get; set; }
        public string Zodiac { get; set; }
        public string Languages { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}