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
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
        public byte[] Picture4 { get; set; }
        public byte[] Picture5 { get; set; }
        public byte[] Picture6 { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }

    }
}