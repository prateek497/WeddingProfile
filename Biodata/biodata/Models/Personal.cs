using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Personal
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        //[Required]
        //[RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        //public string StartTimeValue
        //{
        //    get
        //    {
        //        return DateOfBirth ? DateOfBirth.Value.ToString("hh:mm tt") : string.Empty;
        //    }

        //    set
        //    {
        //        DateOfBirth = DateTime.Parse(value);
        //    }
        //}

        public string Birthplace { get; set; }

        public string Gothra { get; set; }

        public string Complexion { get; set; }

        public string CurrentCity { get; set; }
    }
}