using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
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
        [HttpGet]
        public ActionResult Dashboard()
        {
            return View(new Dashboard()
            {
                Login = new Login()
            });
        }

        [HttpPost]
        public ActionResult Dashboard(Dashboard model)
        {
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult SignIn(Login model)
        {
            if (model.SignIn == null) return null;

            if (ModelState.IsValid)
            {
                if (model.IsValid(model.SignIn.Email, model.SignIn.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.SignIn.Email, true);
                }
            }

            return RedirectToAction("Dashboard");
        }

        public ActionResult SignOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Dashboard");
        }

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

        public ViewResult _Basic()
        {
            var entities = new BiodataDb();
            var userId = Support.GetUserId("admin@biodata.com", entities);

            if (userId > 0)
            {
                var pdfModel = new PdfGeneratorModel();
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
                }).ToList();

                return View(pdfModel);
            }

            return View(new PdfGeneratorModel());
        }

        [HttpPost]
        public FileResult _Basic(PdfGeneratorModel model)
        {
            string fileName = "prateek.pdf"; //model.PersonalData.Name.Replace(" ", "");
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Download\" + fileName;
            try
            {
                if (Request.Url != null) PDFGenerator.PdfGenerator.Generate(Request.Url.ToString(), filePath);
            }
            catch (Exception)
            {
            }


            //DownloadFile download = new DownloadFile(fileName, filePath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            //HtmlToPdfConverter pdfConverter = new HtmlToPdfConverter();
            //var pdfBytes = pdfConverter.GeneratePdf(html);
            //Response.ContentType = "application/pdf";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.AddHeader("Content-Disposition", "Inline; filename=TEST.pdf");
            //Response.BinaryWrite(pdfBytes);
            //Response.Flush();
            //Response.End();
            //PDFGenerator.PdfGenerator.Generate("http://localhost:49183", "F:\testpdf.pdf");

            return File(filePath, "application/pdf", fileName);
        }

        private static string RenderPartialToString(Controller controller, string partialViewName, object model, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName);

            if (result.View != null)
            {
                controller.ViewData.Model = model;
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter output = new HtmlTextWriter(sw))
                    {
                        ViewContext viewContext = new ViewContext(controller.ControllerContext, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }

            return String.Empty;
        }
    }
}