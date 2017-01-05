using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Models
{
    public static class Support
    {
        public static List<string> RelationshipList()
        {
            return new List<string> { "Self", "Father", "Mother", "Brother", "Sister", "Friend", "Other Relative", "Grandfather" };
        }
    }
}