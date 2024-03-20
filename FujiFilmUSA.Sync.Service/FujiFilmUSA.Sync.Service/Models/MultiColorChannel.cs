using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class MultiColorChannel
    {
        [JsonProperty("channelID")]
        public int ChannelID { get; set; }
        [JsonProperty("channelName")]
        public string ChannelName { get; set; }
        [JsonProperty("cmyk_C")]
        public double Cmyk_C { get; set; }
        [JsonProperty("cmyk_M")]
        public double Cmyk_M { get; set; }
        [JsonProperty("cmyk_Y")]
        public double Cmyk_Y { get; set; }
        [JsonProperty("cmyk_K")]
        public double Cmyk_K { get; set; }
    }
}