using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Religious
    {
        [Required(ErrorMessage = "Religion is required", AllowEmptyStrings = false)]
        public string Religion { get; set; }

        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Caste { get; set; }

        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Gotra { get; set; }

        public string Zodiac { get; set; }

        public string Languages { get; set; }

        [StringLength(30, MinimumLength = 1, ErrorMessage = "Mother tongue cannot be longer than 30 characters.")]
        public string MotherTongue { get; set; }

        public IEnumerable ReligionList { get; set; }

        public IEnumerable ZodiacList { get; set; }

        public IEnumerable LanguagesList { get; set; }
    }
}