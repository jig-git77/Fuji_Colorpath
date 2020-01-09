using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient
{
    [Serializable]
    public class Patch
    {

        [JsonProperty("patchID")]
        public int PatchID { get; set; }
        [JsonProperty("colorName")]
        public string ColorName { get; set; }
        [JsonProperty("cmyk_C")]
        public double Cmyk_C { get; set; }
        [JsonProperty("cmyk_M")]
        public double Cmyk_M { get; set; }
        [JsonProperty("cmyk_Y")]
        public double Cmyk_Y { get; set; }
        [JsonProperty("cmyk_K")]
        public double Cmyk_K { get; set; }
        [JsonProperty("spot1")]
        public double Spot1 { get; set; }
        [JsonProperty("cie_L")]
        public double Cie_L { get; set; }
        [JsonProperty("cie_a")]
        public double Cie_a { get; set; }
        [JsonProperty("cie_b")]
        public double Cie_b { get; set; }
        [JsonProperty("p_cie_L")]
        public double p_Cie_L { get; set; }
        [JsonProperty("p_cie_a")]
        public double p_Cie_a { get; set; }
        [JsonProperty("p_cie_b")]
        public double p_Cie_b { get; set; }
        [JsonProperty("spot2")]
        public double Spot2 { get; set; }
    }
}
