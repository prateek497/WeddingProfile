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
<<<<<<< HEAD
            return View();
        }

        public ViewResult _Basic()
        {
=======
            //BiodataDb db = new BiodataDb();
            //db.Users.Add(
            //    new User
            //    {
            //        Email = "prabakar@gmail.com",
            //        Password = "bajrangbali"
            //    });
            //db.SaveChanges();
>>>>>>> 39a3b5ffcefca67372d0de7054b80bb044ea7389
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
