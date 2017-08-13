using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using biodata.Database;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace biodata.Helper
{
    public static class Support
    {
        public static List<string> RelationshipList()
        {
            return new List<string>
            {
                "Self",
                "Father",
                "Mother",
                "Brother",
                "Sister",
                "Friend",
                "Other Relative",
                "Grandfather"
            };
        }

        public static List<string> FamilyRelationshipList()
        {
            return new List<string>
            {
                "FATHER",
                "MOTHER",
                "BROTHER",
                "SISTER",
                "FRIEND",
                "OTHER RELATIVE",
                "GRANDFATHER",
                "GRANDMOTHER",
                "BROTHER-IN-LAW"
                ,
                "SISTER-IN-LAW",
                "UNCLE",
                "AUNTY"
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
                "4\"09",
                "4\"10",
                "4\"11",
                "4\"12",
                "5\"01",
                "5\"02",
                "5\"03",
                "5\"04",
                "5\"05",
                "5\"06",
                "5\"07",
                "5\"08",
                "5\"09",
                "5\"10",
                "5\"11",
                "5\"12",
                "6\"01",
                "6\"02",
                "6\"03",
                "6\"04",
                "6\"05",
                "6\"06",
                "6\"07",
                "6\"08",
                "6\"09",
                "6\"10",
                "6\"11",
                "6\"12",
                "7\"01",
                "7\"02",
                "7\"03",
                "7\"04"
            };
        }

        public static List<string> ReligiousList()
        {
            return new List<string> { "Hinduism", "Islam", "Christianity", "Sikhism", "Buddhism", "Jainism", "Judaism" };
        }

        public static List<string> ZodiacList()
        {
            return new List<string>
            {
                "Aquarius",
                "Pisces",
                "Aries",
                "Taurus",
                "Gemini",
                "Cancer",
                "Leo",
                "Virgo",
                "Libra",
                "Scorpio",
                "Sagittarius",
                "Capricorn"
            };
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
            return new List<string>
            {
                "0 - 1Lakh",
                "1 - 5Lakh",
                "5 - 10Lakh",
                "10 - 15Lakh",
                "15 - 20Lakh",
                "20 - 30Lakh",
                "> 30Lakh"
            };
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

        public static void DeleteExistingDataForUser(int userId)
        {
            var entities = new BiodataDb();

            if (userId > 0)
            {
                entities.Educationinfoes.RemoveRange(entities.Educationinfoes.Where(x => x.UserId == userId));
                entities.Contactinfoes.RemoveRange(entities.Contactinfoes.Where(x => x.UserId == userId));
                entities.Culturalinfoes.RemoveRange(entities.Culturalinfoes.Where(x => x.UserId == userId));
                entities.Familyinfoes.RemoveRange(entities.Familyinfoes.Where(x => x.UserId == userId));
                entities.Personalinfoes.RemoveRange(entities.Personalinfoes.Where(x => x.UserId == userId));
                entities.Pictures.RemoveRange(entities.Pictures.Where(x => x.UserId == userId));
                entities.Workexperienceinfoes.RemoveRange(entities.Workexperienceinfoes.Where(x => x.UserId == userId));
                entities.SaveChanges();
            }
        }

        public static bool SendEmail(string subject, string body, string toEmail)
        {
            string fromEmail = "brightfuture497@gmail.com";//WebConfigurationManager.AppSettings["fromEmail"];
            string fromEmailPassword = "brightfuture";//WebConfigurationManager.AppSettings["fromEmailPassword"];

            using (var mm = new MailMessage(fromEmail, toEmail))
            {
                mm.Subject = subject;
                mm.Body = body;
                //if (model.Attachment.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(model.Attachment.FileName);
                //    mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                //}
                mm.IsBodyHtml = false;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCred = new NetworkCredential(fromEmail, fromEmailPassword);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = 587;
                    try
                    {
                        smtp.Send(mm);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Bitmap CropAtRect(Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            if (img.Width > img.Height)
            {
                cropArea.Width = img.Height;
                cropArea.Height = img.Height;
            }
            else
            {
                cropArea.Width = img.Width;
                cropArea.Height = img.Width;
            }
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height, string colorString)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            ColorConverter colorCon = new ColorConverter();
            Color color = (Color)colorCon.ConvertFromString(colorString);
            grPhoto.Clear(color);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}