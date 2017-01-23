using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Religious
    {
        public string Religion { get; set; }

        public string Caste { get; set; }

        public string Gotra { get; set; }

        public string Zodiac { get; set; }

        public string Languages { get; set; }

        public IEnumerable ReligionList { get; set; }

        public IEnumerable ZodiacList { get; set; }

        public IEnumerable LanguagesList { get; set; }
    }
}