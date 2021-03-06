using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace biodata.Database.Tables
{
    public sealed class User
    {
        public User()
        {
            Contactinfoes = new HashSet<ContactInfo>();
            Culturalinfoes = new HashSet<CulturalInfo>();
            Educationinfoes = new HashSet<EducationInfo>();
            Familyinfoes = new HashSet<FamilyInfo>();
            Personalinfoes = new HashSet<PersonalInfo>();
            Pictures = new HashSet<Picture>();
            Userprofiles = new HashSet<UserProfile>();
            Workexperienceinfoes = new HashSet<WorkExperienceInfo>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public HashSet<ContactInfo> Contactinfoes { get; set; }
        public HashSet<CulturalInfo> Culturalinfoes { get; set; }
        public HashSet<EducationInfo> Educationinfoes { get; set; }
        public HashSet<FamilyInfo> Familyinfoes { get; set; }
        public HashSet<PersonalInfo> Personalinfoes { get; set; }
        public HashSet<Picture> Pictures { get; set; }
        public HashSet<UserProfile> Userprofiles { get; set; }
        public HashSet<WorkExperienceInfo> Workexperienceinfoes { get; set; }

    }
}