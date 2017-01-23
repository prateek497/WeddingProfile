using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biodata.Database;

namespace biodata.Models
{
    public static class Support
    {
        public static List<string> RelationshipList()
        {
            return new List<string> { "Self", "Father", "Mother", "Brother", "Sister", "Friend", "Other Relative", "Grandfather" };
        }

        public static List<string> ComplexionList()
        {
            return new List<string> { "Weatish", "Fair", "Black" };
        }

        public static List<string> HeightList()
        {
            return new List<string>
            {
                "4\"11", "4\"12", "5\"01", "5\"02", "5\"03", "5\"04", "5\"05", "5\"06", "5\"07", "5\"08", "5\"09",
                "5\"10", "5\"11", "5\"12", "6\"01", "6\"02", "6\"03", "6\"04", "6\"05", "6\"06", "6\"07", "6\"08",
            };
        }

        public static int GetUserId(string email, BiodataDb entities)
        {
            var user = entities.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user != null ? user.Id : 0;
        }
    }
}