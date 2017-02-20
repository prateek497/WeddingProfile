using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public byte[] PictureBytes { get; set; }
        public bool IsProfile { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}