using System;
using System.Collections.Generic;
using System.IO;
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

        public static List<string> FamilyRelationshipList()
        {
            return new List<string>
            {
                "FATHER", "MOTHER", "BROTHER", "SISTER", "FRIEND", "OTHER RELATIVE", "GRANDFATHER","GRANDMOTHER","BROTHER-IN-LAW"
                ,"SISTER-IN-LAW","UNCLE","AUNTY"
            };
        }

        public static List<string> ComplexionList()
        {
            return new List<string> { "Weatish", "Fair", "Black" };
        }

        public static List<string> HeightList()
        {
            return new List<string>
            {
                "4\"09", "4\"10","4\"11", "4\"12", "5\"01", "5\"02", "5\"03", "5\"04", "5\"05", "5\"06", "5\"07", "5\"08", "5\"09",
                "5\"10", "5\"11", "5\"12", "6\"01", "6\"02", "6\"03", "6\"04", "6\"05", "6\"06", "6\"07", "6\"08","6\"09","6\"10",
                "6\"11", "6\"12", "7\"01", "7\"02", "7\"03", "7\"04"
            };
        }

        public static List<string> ReligiousList()
        {
            return new List<string> { "Hinduism", "Islam", "Christianity", "Sikhism", "Buddhism", "Jainism", "Judaism" };
        }

        public static List<string> ZodiacList()
        {
            return new List<string> { "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn" };
        }

        public static List<string> LanguagesList()
        {
            return new List<string> { "English", "Hindi", "Kannada", "Telugu", "Bhojpori", "Tamil", "Bengali" };
        }

        public static List<string> MaritalStatusList()
        {
            return new List<string> { "Never Married", "Divorced", "Seperated" };
        }

        public static List<string> DietList()
        {
            return new List<string> { "Vegetarian", "Non-Vegetarian", "Eggetarian" };
        }

        public static List<string> DrinkList()
        {
            return new List<string> { "Yes", "No", "Occasinally" };
        }

        public static List<string> SmokeList()
        {
            return new List<string> { "Yes", "No", "Occasinally" };
        }

        public static List<string> EducationList()
        {
            return new List<string> { "GRADUATE", "POST-GRADUATE", "DOCRATE" };
        }

        public static List<string> AnnualIncomeList()
        {
            return new List<string> { "0 - 1Lakh", "1 - 5Lakh", "5 - 10Lakh", "10 - 15Lakh", "15 - 20Lakh", "> 20Lakh" };
        }

        public static int GetUserId(string email, BiodataDb entities)
        {
            var user = entities.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user != null ? user.Id : 0;
        }

        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}