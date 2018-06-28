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
        [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 25 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required", AllowEmptyStrings = false)]
        public string DateOfBirth { get; set; }

        public DateTime DateOfBirthDb { get; set; }

        public DateTime DateOfTimeDb { get; set; }

        [Required(ErrorMessage = "Time of birth is required", AllowEmptyStrings = false)]
        public string TimeOfBirth { get; set; }

        [Required(ErrorMessage = "Birth place is required", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Birth place cannot be longer than 30 characters.")]
        public string Birthplace { get; set; }

        //[Required(ErrorMessage = "Birth state is required", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Birthstate { get; set; }

        [Required(ErrorMessage = "Complexion is required")]
        public string Complexion { get; set; }

        public string CurrentCity { get; set; }

        public string CurrentState { get; set; }

        [Required(ErrorMessage = "Height is required")]
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

        public string Facebook { get; set; }

        public string Linkedin { get; set; }
        
        public string Instagram { get; set; }
        
        public string Twitter { get; set; }
        
        public string Quora { get; set; }
    }
}