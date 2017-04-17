using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using biodata.Database;

namespace biodata.Models
{
    public class Login
    {
        public SignIn SignIn { get; set; }

        public SignUp SignUp { get; set; }

        public ForgotPassword ForgotPassword { get; set; }

        public bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            try
            {
                using (var db = new BiodataDb())
                {
                    var user = db.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null) if (user.Password == crypto.Compute(password, user.PasswordSalt)) isValid = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToFile(ex.Message.ToString());
            }

            return isValid;
        }

        public bool IsUserExists(string email)
        {
            using (var db = new BiodataDb())
            {
                var user = db.Users.Where(x => x.Email.Equals(email)).ToList();
                if (user.Count < 1) return true;
                return false;
            }
        }
    }

    public static class Logger
    {
        public static void WriteToFile(string text)
        {
            string physicalPath = HostingEnvironment.ApplicationPhysicalPath;

            physicalPath = Path.Combine(physicalPath, @"Notes.txt");
            //if (!File.Exists(physicalPath)) File.CreateText(physicalPath);
            using (StreamWriter writer = File.AppendText(physicalPath))
            {
                writer.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + " " + text);
                writer.WriteLine("");
                writer.Close();
            }

        }

    }
}