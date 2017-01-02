using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using biodata.Database;
using biodata.Database.Tables;
using biodata.Models;
using NReco.PdfGenerator;

namespace biodata.Controllers
{
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
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult SignUp(Login model)
        {
            if (model.SignUp == null) return null;
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