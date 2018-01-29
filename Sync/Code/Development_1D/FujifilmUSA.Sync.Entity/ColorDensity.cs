using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ColorDensity
    {
        public int MeasurementID { get; set; }
        public string Color { get; set; }
        public decimal Density { get; set; }
        public decimal ΔE { get; set; }
        public decimal ΔElimit { get; set; }
    }

    public class CMYKDensity
    {
        public CMYKDensity()
        {
            CyanList = new List<ColorDensity>();
            MagentaList = new List<ColorDensity>();
            YellowList = new List<ColorDensity>();
            BlackList = new List<ColorDensity>();
        }
        public List<ColorDensity> CyanList { get; set; }
        public List<ColorDensity> MagentaList { get; set; }
        public List<ColorDensity> YellowList { get; set; }
        public List<ColorDensity> BlackList { get; set; }
    }
}
