using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    [Serializable]
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

    [Serializable]
    public class Wedge
    {
        public Wedge()
        {
            listPatch = new List<Patch>();
        }
        public List<Patch> listPatch { get; set; }
    }

    [Serializable]
    public class PdfConfigurations
    {
        [JsonProperty("numberOfPatches")]
        public int NumberOfPatches { get; set; }
        [JsonProperty("chartPages")]
        public int ChartPages { get; set; }
        [JsonProperty("patchWidth")]
        public int PatchWidth { get; set; }
        [JsonProperty("patchHeight")]
        public int PatchHeight { get; set; }
        [JsonProperty("chartBorder")]
        public int ChartBorder { get; set; }
        [JsonProperty("chartSizeW")]
        public int ChartSizeW { get; set; }
        [JsonProperty("chartSizeH")]
        public int ChartSizeH { get; set; }
        [JsonProperty("rowsPerChart")]
        public int RowsPerChart { get; set; }
        [JsonProperty("columsPerChart")]
        public int ColumsPerChart { get; set; }
        [JsonProperty("vGap")]
        public int Vgap { get; set; }
        [JsonProperty("hGap")]
        public int Hgap { get; set; }
        [JsonProperty("gapLeval")]
        public int GapLeval { get; set; }
        [JsonProperty("previousL")]
        public double PreviousL { get; set; }
        [JsonProperty("chartName")]
        public string ChartName { get; set; }
        [JsonProperty("profileName")]
        public string ProfileName { get; set; }
    }

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
    }
}
