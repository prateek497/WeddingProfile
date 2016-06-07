using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            CreatePdf();
        }

        #region extra code
        //public void PdfEngine()
        //{
        //    Rectangle rec = new Rectangle(PageSize.A4);
        //    var doc1 = new Document(rec);
        //    rec.BackgroundColor = new BaseColor(System.Drawing.Color.WhiteSmoke);
        //    var path = System.Reflection.Assembly.GetExecutingAssembly().Location; //Server.MapPath("PDFs");
        //    var exists = Directory.Exists(path);
        //    if (!exists) Directory.CreateDirectory(path);
        //    PdfWriter.GetInstance(doc1, new FileStream(path + "/1.pdf", FileMode.Create, FileAccess.Write, FileShare.None));

        //    doc1.Open();
        //    Paragraph para =
        //        new Paragraph(
        //            "This is a text which is like a paragraph")
        //        {
        //            Alignment = Element.ALIGN_JUSTIFIED
        //        };

        //    doc1.AddTitle("Hello World example");
        //    doc1.AddSubject("This is an Example 4 of Chapter 1 of Book 'iText in Action'");
        //    doc1.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
        //    doc1.AddCreator("iTextSharp 5.4.4");
        //    doc1.AddAuthor("Prateek Gangwar");
        //    doc1.AddHeader("Nothing", "This is a header");
        //    doc1.Add(para);

        //    Image jpg = Image.GetInstance(Server.MapPath("Images") + "\\calender.png");
        //    //Resize image depend upon your need
        //    jpg.ScaleToFit(70f, 60f);
        //    //Give space before image
        //    jpg.SpacingBefore = 10f;
        //    //Give some space after the image
        //    jpg.SpacingAfter = 1f;
        //    jpg.Alignment = Element.ALIGN_LEFT;

        //    doc1.Add(jpg);
        //    doc1.Close();
        //}
        #endregion

        private static void CreatePdf()
        {
            var fileCreationDatetime = DateTime.Now;
            var fileName = string.Format("\\{0}.pdf", fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss"));
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, 48);
            var name = Path.GetDirectoryName(path);
            var pdfPath = name + "biodata.pdf";//fileName;
            using (var msReport = new FileStream(pdfPath, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 5f, 5f, 150f, 5f))
                {
                    try
                    {
                        // step 2
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new Events();
                        //open the stream 
                        pdfDoc.Open();
                        for (int i = 0; i < 1; i++)
                        {
                            
                            var para = new Paragraph("Hello world. Checking Header Footer", new Font(Font.FontFamily.HELVETICA, 16)) { Alignment = Element.ALIGN_CENTER };
                            pdfDoc.Add(para);
                            pdfDoc.NewPage();
                        }
                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        //handle exception
                    }
                }
            }
        }

        public class Events : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                PdfPTable table = new PdfPTable(1);
                table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; //this centers [table]
                var table2 = new PdfPTable(2);

                var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var imgPath = Path.GetDirectoryName(path) + "\\me.jpg";

                //logo
                var imghead = Image.GetInstance(imgPath);
                imghead.ScaleToFit(64f, 64f);

                var cell2 = new PdfPCell(imghead) { Colspan = 2 };

                table2.AddCell(cell2);

                //title
                //cell2 = new PdfPCell(new Phrase("\nTITLE", new Font(Font.NORMAL, 16, Font.BOLD | Font.UNDERLINE)))
                //{
                //    HorizontalAlignment = Element.ALIGN_CENTER,
                //    Colspan = 2
                //};
                //table2.AddCell(cell2);

                var cell = new PdfPCell(table2);
                table.AddCell(cell);

                table.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - 36, writer.DirectContent);
            }
        }



        //public class TextEvents : PdfPageEventHelper
        //{
        //    // This is the contentbyte object of the writer
        //    PdfContentByte cb;
        //    // we will put the final number of pages in a template
        //    PdfTemplate _headerTemplate, _footerTemplate;
        //    // this is the BaseFont we are going to use for the header / footer
        //    BaseFont bf = null;
        //    // This keeps track of the creation time
        //    DateTime _printTime = DateTime.Now;

        //    PdfTemplate total;

        //    #region Properties
        //    public string Header { get; set; }
        //    #endregion

        //    public override void OnStartPage(PdfWriter writer, Document document)
        //    {
        //        var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //        string imgPath = Path.GetDirectoryName(path) + "\\me.jpg";

        //        //Header Image
        //        Image imghead = Image.GetInstance(imgPath);
        //        imghead.ScaleToFit(64f, 64f);

        //        //Give space before image
        //        imghead.SpacingBefore = 10f;
        //        //Give some space after the image
        //        imghead.SpacingAfter = 1f;
        //        imghead.Alignment = Element.ALIGN_LEFT;

        //        imghead.SetAbsolutePosition(50, 10);

        //        PdfContentByte cbhead = writer.DirectContent;
        //        PdfTemplate tp = cbhead.CreateTemplate(273, 95);
        //        tp.AddImage(imghead);

        //        cbhead.AddTemplate(tp, 0, 842 - 95);

        //        bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        //        PdfContentByte cb = writer.DirectContent;
        //        document.SetMargins(35, 35, 100, 82);

        //        string text = "Developed by ";
        //        float textSize = 9;
        //        cb.RestoreState();
        //        base.OnStartPage(writer, document);
        //    }

        //    public override void OnOpenDocument(PdfWriter writer, Document document)
        //    {
        //        try
        //        {
        //            _printTime = DateTime.Now;
        //            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //            cb = writer.DirectContent;
        //            _headerTemplate = cb.CreateTemplate(100, 100);
        //            _footerTemplate = cb.CreateTemplate(50, 50);
        //        }
        //        catch (DocumentException)
        //        {

        //        }
        //        catch (IOException)
        //        {

        //        }
        //    }

        //    public override void OnEndPage(PdfWriter writer, Document document)
        //    {
        //        base.OnEndPage(writer, document);

        //        Font baseFontNormal = new Font(Font.FontFamily.HELVETICA, 12f, Font.NORMAL, BaseColor.BLACK);

        //        Font baseFontBig = new Font(Font.FontFamily.HELVETICA, 12f, Font.BOLD, BaseColor.BLACK);

        //        Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

        //        //Create PdfTable object
        //        PdfPTable pdfTab = new PdfPTable(3);

        //        var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //        string imgPath = Path.GetDirectoryName(path);
        //        Image jpg = Image.GetInstance(imgPath + "\\me.jpg");
        //        //Resize image depend upon your need
        //        jpg.ScaleToFit(70f, 60f);
        //        //Give space before image
        //        jpg.SpacingBefore = 10f;
        //        //Give some space after the image
        //        jpg.SpacingAfter = 1f;
        //        jpg.Alignment = Element.ALIGN_LEFT;

        //        //We will have to create separate cells to include image logo and 2 separate strings
        //        //Row 1
        //        PdfPCell pdfCell1 = new PdfPCell();
        //        PdfPCell pdfCell2 = new PdfPCell(jpg);
        //        PdfPCell pdfCell3 = new PdfPCell();
        //        String text = "Page " + writer.PageNumber + " of ";


        //        //Add paging to header
        //        {
        //            cb.BeginText();
        //            cb.SetFontAndSize(bf, 12);
        //            cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
        //            //cb.ShowText(text);
        //            cb.EndText();
        //            float len = bf.GetWidthPoint(text, 12);
        //            //Adds "12" in Page 1 of 12
        //            cb.AddTemplate(_headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
        //        }
        //        //Add paging to footer
        //        {
        //            cb.BeginText();
        //            cb.SetFontAndSize(bf, 12);
        //            cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));
        //            cb.ShowText(text);
        //            cb.EndText();
        //            float len = bf.GetWidthPoint(text, 12);
        //            cb.AddTemplate(_footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));
        //        }
        //        //Row 2
        //        PdfPCell pdfCell4 = new PdfPCell(new Phrase("Sub Header Description", baseFontNormal));
        //        //Row 3

        //        PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + _printTime.ToShortDateString(), baseFontBig));
        //        PdfPCell pdfCell6 = new PdfPCell();
        //        PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));

        //        //set the alignment of all three cells and set border to 0
        //        pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
        //        pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;

        //        pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
        //        pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
        //        pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
        //        pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
        //        pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
        //        pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;
        //        pdfCell4.Colspan = 3;

        //        pdfCell1.Border = 0;
        //        pdfCell2.Border = 0;
        //        pdfCell3.Border = 0;
        //        pdfCell4.Border = 0;
        //        pdfCell5.Border = 0;
        //        pdfCell6.Border = 0;
        //        pdfCell7.Border = 0;

        //        //add all three cells into PdfTable
        //        //pdfTab.AddCell(pdfCell1);
        //        //pdfTab.AddCell(pdfCell2);
        //        //pdfTab.AddCell(pdfCell3);
        //        //pdfTab.AddCell(pdfCell4);
        //        //pdfTab.AddCell(pdfCell5);
        //        //pdfTab.AddCell(pdfCell6);
        //        //pdfTab.AddCell(pdfCell7);

        //        pdfTab.TotalWidth = document.PageSize.Width - 80f;
        //        pdfTab.WidthPercentage = 70;
        //        //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;

        //        //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
        //        //first param is start row. -1 indicates there is no end row and all the rows to be included to write
        //        //Third and fourth param is x and y position to start writing
        //        pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
        //        //set pdfContent value

        //        //Move the pointer and draw line to separate header section from rest of page
        //        cb.MoveTo(40, document.PageSize.Height - 100);
        //        cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
        //        cb.Stroke();

        //        //Move the pointer and draw line to separate footer section from rest of page
        //        cb.MoveTo(40, document.PageSize.GetBottom(50));
        //        cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
        //        cb.Stroke();
        //    }

        //    public override void OnCloseDocument(PdfWriter writer, Document document)
        //    {
        //        base.OnCloseDocument(writer, document);

        //        _headerTemplate.BeginText();
        //        _headerTemplate.SetFontAndSize(bf, 12);
        //        _headerTemplate.SetTextMatrix(0, 0);
        //        //_headerTemplate.ShowText((writer.PageNumber - 1).ToString());
        //        _headerTemplate.EndText();

        //        _footerTemplate.BeginText();
        //        _footerTemplate.SetFontAndSize(bf, 12);
        //        _footerTemplate.SetTextMatrix(0, 0);
        //        _footerTemplate.ShowText((writer.PageNumber - 1).ToString());
        //        _footerTemplate.EndText();
        //    }
        //}
    }
}
