using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biodata.Database;

namespace biodata.Models
{
    public class Login
    {
        public SignIn SignIn { get; set; }

        public SignUp SignUp { get; set; }

        public bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            try
            {
                using (var db = new BiodataDb())
                {
                    var user = db.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                        if (user.Password == crypto.Compute(password, user.PasswordSalt)) isValid = true;
                }
            }
            catch (Exception ex)
            {
                // ignored
            }

            return isValid;
        }
    }
}