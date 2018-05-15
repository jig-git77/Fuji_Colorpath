using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SpotProjectChartSetting
    {
        public int SpotProjectChartSettingID { get; set; }
        public int SpotProjectID { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public decimal ChartSizeX { get; set; }
        public decimal ChartSizeY { get; set; }
        public decimal PatchSizeX { get; set; }
        public decimal PatchSizeY { get; set; }
        public string FirstSample { get; set; }
        public string ReadDirection { get; set; }
        public string ReadMode { get; set; }
        public int Pages { get; set; }
        public int RowsPerPage { get; set; }
        public int ChartBorder { get; set; }
        public int Vgap { get; set; }
        public int Hgap { get; set; }
        public int GapLeval { get; set; }
        public int PreviousL { get; set; }
        public int ChartRes { get; set; }
    }
}
