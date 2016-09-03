using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NReco.PdfGenerator;

namespace PDFGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public static class PdfGenerator
    {
        /// <summary>
        /// Generate pdf at local system
        /// </summary>
        /// <param name="url">URL of the page</param>
        /// <param name="path">Where to save</param>
        public static void Generate(string url, string path)
        {
            var htmlToPdf = new HtmlToPdfConverter
            {
                Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 },
                Size = PageSize.A4
            };

            htmlToPdf.GeneratePdfFromFile(url, null, @path);
        }

        /// <summary>
        /// Todo Generate bytes from html code
        /// </summary>
        /// <param name="html">html code</param>
        /// return bytes
        public static void GenerateBytes(string html)
        {
            var htmlToPdf = new HtmlToPdfConverter
            {
                Margins = new PageMargins { Top = 0, Bottom = 0, Left = 0, Right = 0 },
                Size = PageSize.A4
            };

            htmlToPdf.GeneratePdf(html);
        }
    }
}
