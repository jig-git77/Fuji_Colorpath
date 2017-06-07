using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class FileResponse
    {
        //[JsonProperty("isSuccess")]
        //public bool IsSuccess { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("filePath")]
        public string FilePath { get; set; }
    }
}