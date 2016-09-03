﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NReco.PdfGenerator;

namespace biodata.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult _Basic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _Basic(string model)
        {
            PDFGenerator.PdfGenerator.Generate("http://localhost:49183/Home/_Basic", "D:\testpdf.pdf");

            return RedirectToAction("_Basic");
        }
    }
}
