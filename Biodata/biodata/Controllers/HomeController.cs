using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;

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

            //render partial view as string
           // var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(ClientHTML);
           // System.IO.File.WriteAllBytes(@"E:\Practice\biodata\Biodata\testpdf.pdf", pdfBytes);
            return Content(ClientHTML);
        }

    }

    public class ViewModel
    {
        [AllowHtml]
        public StringBuilder ClientHTML { get; set; }
    }

}
