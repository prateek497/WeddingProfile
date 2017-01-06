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

        public static int GetUserId(string email, BiodataDb entities)
        {
            var user = entities.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user != null ? user.Id : 0;
        }
    }
}