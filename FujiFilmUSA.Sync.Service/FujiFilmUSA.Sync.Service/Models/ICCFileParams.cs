using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class IccFileParams
    {        
        public IccFileParams()
        {
            Patches = new List<Patch>();
            Configurations = new PdfConfigurations();
        }

        [JsonProperty("patches")]
        public List<Patch> Patches { get; set; }
        [JsonProperty("configurations")]
        public PdfConfigurations Configurations { get; set; }
    }
}