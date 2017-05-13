using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using biodata.Database;
using biodata.Database.Tables;
using biodata.Helper;
using biodata.Models;
using NReco.PdfGenerator;

namespace biodata.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Dashboard()
        {
            try
            {
                if (TempData["errormessage"] != null)
                    return View(new Dashboard
                    {
                        AlertMessage = TempData["errormessage"].ToString(),
                        Login = new Login()
                    });

                using (var db = new BiodataDb())
                {
                    db.Database.Connection.Open();
                    var user = db.Users.ToList();
                    db.Database.Connection.Close();
                }


            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return View(new Dashboard
                    {
                        AlertMessage = ex.InnerException.ToString(),
                        Login = new Login()
                    });

                return View(new Dashboard
                {
                    AlertMessage = ex.ToString(),
                    Login = new Login()
                });
            }


            return View(new Dashboard()
            {
                Login = new Login()
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Dashboard(Dashboard model)
        {
            return RedirectToAction("Dashboard");
        }


        public ActionResult SignInValidation(string email, string password)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password)) return Content("Something went wrong");

            using (var entities = new BiodataDb())
            {
                var isExists = entities.Users.Any(x => x.Email.Equals(email));
                if (!isExists) return Content("User does not exits");
                var model = new Login();
                var isValid = model.IsValid(email, password);
                if(!isValid) return Content("Username or password is not matched");
            }

            return Content("");
        }

        public ActionResult SignUpValidation(string email)
        {
            if (string.IsNullOrEmpty(email)) return Content("Something went wrong");
            using (var entities = new BiodataDb())
            {
                var isExists = entities.Users.Any(x => x.Email.Equals(email));
                if (isExists) return Content("User already exits");
            }

            return Content("");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(Login model)
        {
            if (model.SignIn == null) return null;

            try
            {
                if (ModelState.IsValid)
                {
                    if (model.IsValid(model.SignIn.Email, model.SignIn.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.SignIn.Email, true);
                    }
                }

                using (var entities = new BiodataDb())
                {
                    int userId = Support.GetUserId(model.SignIn.Email, entities);
                    if (userId > 0) Support.DeleteExistingDataForUser(userId);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex;
            }



            return RedirectToAction("Dashboard");
        }

        [AllowAnonymous]
        public ActionResult SignOff()
        {
            using (var entities = new BiodataDb())
            {
                int userId = Support.GetUserId(User.Identity.Name, entities);
                if (userId > 0) Support.DeleteExistingDataForUser(userId);
            }
            FormsAuthentication.SignOut();
            return RedirectToAction("Dashboard");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(Login model)
        {
            if (model.SignUp == null) return null;

            try
            {
                if (ModelState.IsValid)
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var entities = new BiodataDb();
                    if (model.IsUserExists(model.SignUp.Email))
                    {
                        entities.Users.Add(new User
                        {
                            Name = model.SignUp.Name,
                            Email = model.SignUp.Email,
                            Password = crypto.Compute(model.SignUp.Password),
                            PasswordSalt = crypto.Salt,
                            IsAdmin = true,
                            IsActive = false,
                            CreatedDateTime = DateTime.Now
                        });

                        entities.SaveChanges();

                        FormsAuthentication.SetAuthCookie(model.SignUp.Email, false);
                    }
                    else ModelState.AddModelError("", "User already exists");
                }
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(Login model)
        {
            if (model.ForgotPassword == null) return null;

            if (ModelState.IsValid)
            {
                string password = "password";
                using (var db = new BiodataDb())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var user = db.Users.FirstOrDefault(u => u.Email == model.ForgotPassword.FormatEmail);
                    if (user != null)
                    {
                        user.Password = crypto.Compute("password");
                        user.PasswordSalt = crypto.Salt;
                        db.SaveChanges();
                        if (!string.IsNullOrEmpty(password))
                        {
                            var status = Support.SendEmail("Password Reset", "Here is your new password- " + password, model.ForgotPassword.FormatEmail);
                            if (status)
                            {
                                ModelState.Clear();
                                return View(new Login
                                {
                                    ForgotPassword = new ForgotPassword
                                    {
                                        AlertMessage = "Email sent successfully",
                                        FormatEmail = string.Empty
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                        model.ForgotPassword.AlertMessage = model.ForgotPassword.FormatEmail + " does not exists into our database.";
                        return View(model);
                    }
                }
            }

            model.ForgotPassword.AlertMessage = "Something went wrong";
            return View(model);
        }

        [AllowAnonymous]
        [Authorize]
        public ViewResult _Basic(PdfGeneratorModel model)
        {
            var entities = new BiodataDb();
            //if (Session["UserEmail"] != null) email1 = Convert.ToString(Session["UserEmail"]);
            if (!string.IsNullOrEmpty(model.Email))
            {
                var userId = Support.GetUserId(model.Email, entities);//User.Identity.Name

                if (userId > 0)
                {
                    var pdfModel = new PdfGeneratorModel();
                    pdfModel.CareerData = entities.Workexperienceinfoes.Where(x => x.UserId == userId).Select(z => new Career
                        {
                            Designation = z.Designation,
                            Company = z.Company,
                            Location = z.Location,
                            WorkingFrom = z.TotalExperience,
                            YesWorkExperience = z.IsWorkingExprience,
                            AnnualIncomeText = z.AnnualIncome
                        }).FirstOrDefault();

                    pdfModel.ContactData = entities.Contactinfoes.Where(x => x.UserId == userId).Select(z => new Contact
                    {
                        State = z.State,
                        City = z.City,
                        Name = z.Name,
                        RelationshipText = z.Relationship,
                        Email = z.Email,
                        Phone = z.ContactNumber
                    }).FirstOrDefault();

                    pdfModel.EducationData = entities.Educationinfoes.Where(x => x.UserId == userId).ToList();

                    pdfModel.FamilyData = entities.Familyinfoes.Where(x => x.UserId == userId).Select(z => new Family
                    {
                        State = z.State,
                        City = z.City,
                        CompanyName = z.Company,
                        Designation = z.Designation,
                        JobLocation = z.Joblocation,
                        RelationshipText = z.Relationship,
                        Name = z.Name
                    }).ToList();
                    pdfModel.PersonalData = entities.Personalinfoes.Where(x => x.UserId == userId).Select(z => new Personal
                    {
                        Name = z.Name,
                        Complexion = z.Complexion,
                        CurrentCity = z.CurrentCity,
                        DateOfBirthDb = z.Dob,
                        DateOfTimeDb = z.DobTime,
                        Height = z.Height,
                        Twitter = z.Twitter,
                        Linkedin = z.Linkedin,
                        Instagram = z.Instagram,
                        Facebook = z.Facebook,
                        Quora = z.Quora,
                        Smoke = z.Smoke,
                        Drink = z.Drink,
                        Hobbies = z.Hobbies,
                        Diet = z.Diet,
                        MaritalStatus = z.MaritalStatus,
                    }).FirstOrDefault();
                    pdfModel.ReligiousData =
                        entities.Culturalinfoes.Where(x => x.UserId == userId).Select(z => new Religious
                        {
                            Caste = z.Caste,
                            Gotra = z.Gotra,
                            Languages = z.Languages,
                            Religion = z.Religion,
                            Zodiac = z.Zodiac,
                            MotherTongue = z.MotherTongue
                        }).FirstOrDefault();
                    pdfModel.ProfilePicture =
                        entities.Pictures.Where(x => x.UserId == userId && x.IsProfile).Select(z => new PictureModel
                        {
                            PicBytes = z.PictureBytes
                        }).FirstOrDefault();
                    pdfModel.PictureListData = entities.Pictures.Where(x => x.UserId == userId).Select(z => new PictureModel
                    {
                        PicBytes = z.PictureBytes
                    }).Take(6).ToList();

                    return View(pdfModel);
                }
            }
            return View(new PdfGeneratorModel());
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public FileResult _Basic(string email, PdfGeneratorModel model)
        {
            model.Email = email;
            model = GetPdfGeneratorModel(email);
            string htmlstring = RenderRazorViewToString("_Basic", model);

            var contentBytes = PDFGenerator.PdfGenerator.GenerateBytes(htmlstring);

            string temp = DateTime.Now.Ticks + ".pdf";
            //string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Download\" + temp;
            //try
            //{
            //    if (Request.Url != null)
            //    {
            //        PDFGenerator.PdfGenerator.Generate(Request.Url.ToString(), filePath);
            //    }
            //}
            //catch (Exception)
            //{
            //    // ignored
            //}

            //DownloadFile download = new DownloadFile(fileName, filePath);
            //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            using (var entities = new BiodataDb())
            {
                int userId = Support.GetUserId(email, entities);
                if (userId > 0) Support.DeleteExistingDataForUser(userId);
            }

            return File(contentBytes, "application/pdf", temp);
        }

        private PdfGeneratorModel GetPdfGeneratorModel(string email)
        {
            var entities = new BiodataDb();
            var pdfModel = new PdfGeneratorModel();
            if (!string.IsNullOrEmpty(email))
            {
                var userId = Support.GetUserId(email, entities); //User.Identity.Name

                if (userId > 0)
                {

                    pdfModel.CareerData =
                        entities.Workexperienceinfoes.Where(x => x.UserId == userId).Select(z => new Career
                        {
                            Designation = z.Designation,
                            Company = z.Company,
                            Location = z.Location,
                            WorkingFrom = z.TotalExperience,
                            YesWorkExperience = z.IsWorkingExprience,
                            AnnualIncomeText = z.AnnualIncome
                        }).FirstOrDefault();

                    pdfModel.ContactData = entities.Contactinfoes.Where(x => x.UserId == userId).Select(z => new Contact
                    {
                        State = z.State,
                        City = z.City,
                        Name = z.Name,
                        RelationshipText = z.Relationship,
                        Email = z.Email,
                        Phone = z.ContactNumber
                    }).FirstOrDefault();

                    pdfModel.EducationData = entities.Educationinfoes.Where(x => x.UserId == userId).ToList();

                    pdfModel.FamilyData = entities.Familyinfoes.Where(x => x.UserId == userId).Select(z => new Family
                    {
                        State = z.State,
                        City = z.City,
                        CompanyName = z.Company,
                        Designation = z.Designation,
                        JobLocation = z.Joblocation,
                        RelationshipText = z.Relationship,
                        Name = z.Name
                    }).ToList();
                    pdfModel.PersonalData =
                        entities.Personalinfoes.Where(x => x.UserId == userId).Select(z => new Personal
                        {
                            Name = z.Name,
                            Complexion = z.Complexion,
                            CurrentCity = z.CurrentCity,
                            DateOfBirthDb = z.Dob,
                            DateOfTimeDb = z.DobTime,
                            Height = z.Height,
                            Twitter = z.Twitter,
                            Linkedin = z.Linkedin,
                            Instagram = z.Instagram,
                            Facebook = z.Facebook,
                            Quora = z.Quora,
                            Smoke = z.Smoke,
                            Drink = z.Drink,
                            Hobbies = z.Hobbies,
                            Diet = z.Diet,
                            MaritalStatus = z.MaritalStatus,
                        }).FirstOrDefault();
                    pdfModel.ReligiousData =
                        entities.Culturalinfoes.Where(x => x.UserId == userId).Select(z => new Religious
                        {
                            Caste = z.Caste,
                            Gotra = z.Gotra,
                            Languages = z.Languages,
                            Religion = z.Religion,
                            Zodiac = z.Zodiac,
                            MotherTongue = z.MotherTongue
                        }).FirstOrDefault();
                    pdfModel.ProfilePicture =
                        entities.Pictures.Where(x => x.UserId == userId && x.IsProfile).Select(z => new PictureModel
                        {
                            PicBytes = z.PictureBytes
                        }).FirstOrDefault();
                    pdfModel.PictureListData =
                        entities.Pictures.Where(x => x.UserId == userId).Select(z => new PictureModel
                        {
                            PicBytes = z.PictureBytes
                        }).Take(6).ToList();


                }

            }
            return pdfModel;

        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpGet]
        public ActionResult ContactUs(string email, string message)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
            {
                var stutus = Support.SendEmail("New message from User", "Email- " + email + " Message: " + message, "prateek497@gmail.com");
                if (stutus) return Json(new { emailsent = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { emailsent = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Sample1()
        {
            return View();
        }

        public ActionResult Sample2()
        {
            return View();
        }
    }
}