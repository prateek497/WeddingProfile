using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Personal
    {
        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public DateTime DateOfBirthDb { get; set; }

        public DateTime DateOfTimeDb { get; set; }

        //[RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        public string TimeOfBirth
        {
            get;
            set;

            //get
            //{
            //    return DateOfBirth != null ? DateOfBirth.ToString("hh:mm tt") : string.Empty;
            //}

            //set
            //{
            //    DateOfBirth = DateTime.Parse(value);
            //}
        }

        public string Birthplace { get; set; }

        public string Complexion { get; set; }

        public string CurrentCity { get; set; }

        public string Height { get; set; }

        public string MaritalStatus { get; set; }

        public string Diet { get; set; }

        public string Drink { get; set; }

        public string Smoke { get; set; }

        public string Hobbies { get; set; }

        public IEnumerable ComplexionList { get; set; }

        public IEnumerable HeightList { get; set; }

        public IEnumerable MaritalStatusList { get; set; }

        public IEnumerable DietList { get; set; }

        public IEnumerable DrinkList { get; set; }

        public IEnumerable SmokeList { get; set; }



    }
}