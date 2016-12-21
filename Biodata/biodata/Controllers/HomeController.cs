using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using biodata.Database;
using biodata.Database.Tables;
using NReco.PdfGenerator;

namespace biodata.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Dashboard()
        {

            return View();
        }

        public ViewResult _Basic()
        {

            //BiodataDb db = new BiodataDb();
            //db.Users.Add(
            //    new User
            //    {
            //        Email = "prabakar@gmail.com",
            //        Password = "bajrangbali"
            //    });
            //db.SaveChanges();

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
