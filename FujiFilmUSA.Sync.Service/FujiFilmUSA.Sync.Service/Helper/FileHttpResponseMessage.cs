using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Helper
{
    public class FileHttpResponseMessage : HttpResponseMessage
    {
        public string filePath;
        public FileHttpResponseMessage() { }
        public FileHttpResponseMessage(string filePath)
        {
            this.filePath = filePath;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Content.Dispose();

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}