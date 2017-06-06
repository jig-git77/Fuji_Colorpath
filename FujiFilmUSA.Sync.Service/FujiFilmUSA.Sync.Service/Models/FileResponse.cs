using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class FileResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}