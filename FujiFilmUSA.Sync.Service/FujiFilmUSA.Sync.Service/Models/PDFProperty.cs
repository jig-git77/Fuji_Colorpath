using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class PDFProperty
    {
        public int numberOfPatches { get; set; }
        public int chartPages { get; set; }
        public int patchWidth { get; set; }
        public int patchHeight { get; set; }
        public int chartBorder { get; set; }
        public int chartSizeW { get; set; }
        public int chartSizeH { get; set; }
        public int rowsPerChart { get; set; }
        public int columsPerChart { get; set; }
        public int vGap { get; set; }
        public int hGap { get; set; }
        public int gapLeval { get; set; }
        public double previousL { get; set; }
        public string chartName { get; set; }
    }
}