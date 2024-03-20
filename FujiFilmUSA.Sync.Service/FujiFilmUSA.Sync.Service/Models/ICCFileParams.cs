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
            MultiColorChannels = new List<MultiColorChannel>();
        }

        [JsonProperty("patches")]
        public List<Patch> Patches { get; set; }
        [JsonProperty("configurations")]
        public PdfConfigurations Configurations { get; set; }
        [JsonProperty("multiColorChannels")]
        public List<MultiColorChannel> MultiColorChannels { get; set; }
    }
}