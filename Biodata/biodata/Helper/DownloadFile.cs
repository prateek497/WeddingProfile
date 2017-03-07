using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace biodata.Helper
{
    public class DownloadFile : IHttpHandler
    {
        private string FileName { get; set; }
        private string FilePath { get; set; }

        public DownloadFile(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
        }

        public void ProcessRequest(HttpContext context)
        {
            var response = HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(FilePath);
            response.Flush();
            response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}