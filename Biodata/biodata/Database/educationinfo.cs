//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace biodata.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class educationinfo
    {
        public int Id { get; set; }
        public string HigherSecondaryYear { get; set; }
        public string HigherSecondarySchool { get; set; }
        public string GraduateDegree { get; set; }
        public string GraduatedYear { get; set; }
        public string GraduateCollege { get; set; }
        public string GraduateUniversity { get; set; }
        public string PostGraduateDegree { get; set; }
        public string PostGraduateYear { get; set; }
        public string PostGraduateCollege { get; set; }
        public string PostGraduateUniversity { get; set; }
        public string DoctarateDegree { get; set; }
        public string DoctarateYear { get; set; }
        public string DoctarateCollege { get; set; }
        public string DoctarateUniversity { get; set; }
        public string AnnualIncome { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual user user { get; set; }
    }
}
