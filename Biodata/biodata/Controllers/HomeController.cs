using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
using biodata.Database;
using biodata.Database.Tables;
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
                    FormsAuthentication.SetAuthCookie(model.SignIn.Email, false);
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
            return View();
        }

        [HttpPost]
        public ActionResult _Basic(string model)
        {
            PDFGenerator.PdfGenerator.Generate("http://localhost:49183", "D:\testpdf.pdf");

            return RedirectToAction("_Basic");
        }
    }
}