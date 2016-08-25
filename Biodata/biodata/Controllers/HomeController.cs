using System;
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
        public ActionResult Index()
        {
            return View(new ViewModel() { ClientHTML = new StringBuilder() });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GenaratePdf(string ClientHTML, ViewModel model)
        {
            //var rawHtml = Server.HtmlDecode(ClientHTML);

            var pdfBytes = (new HtmlToPdfConverter { Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 } }).GeneratePdf(ClientHTML);
            System.IO.File.WriteAllBytes(@"E:\Practice\biodata\Biodata\testpdf.pdf", pdfBytes);

            //E:\Practice\biodata\Biodata\testpdf.pdf  D:\testpdf.pdf

            //var htmlToPdf = new HtmlToPdfConverter
            //{
            //    Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 },
            //    Size = PageSize.A4
            //};
            //htmlToPdf.GeneratePdfFromFile("http://datecalculator.in/", null, @"D:\testpdf.pdf");

            return Content(ClientHTML);
        }


        public PartialViewResult _Basic()
        {
            return PartialView(new ViewModel { ClientHTML = new StringBuilder() });
        }

        [HttpPost]
        public ActionResult _Basic(string model)
        {
            var htmlToPdf = new HtmlToPdfConverter
            {
                Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 },
                Size = PageSize.A4
            };
            htmlToPdf.GeneratePdfFromFile("http://localhost:49183/Home/_Basic", null, @"E:\Practice\biodata\Biodata\testpdf.pdf");
            return null;
        }

    }

    public class ViewModel
    {
        [AllowHtml]
        public StringBuilder ClientHTML { get; set; }
    }

}
