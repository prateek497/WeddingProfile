using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public class Families
    {
        public List<Family> FamilyList { get; set; }

        public Family FamilyMember { get; set; }
    }

    public class Family
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Designation { get; set; }

        public string CompanyName { get; set; }

        public string JobLocation { get; set; }

        public string RelationshipText { get; set; }

        public List<string> RelationshipList { get; set; }
    }
}