using System.Xml;
using System;

namespace FujifilmUSA.Sync.Entity
{
    public class ChartSetting
    {
        public Decimal PatchSizeX { get; set; }
        public Decimal PatchSizeY { get; set; }
        public Decimal BarSizeX { get; set; }
        public Decimal BarSizeY { get; set; }
        public Decimal DistToFirstLineY { get; set; }
        public Decimal MarkerDistance { get; set; }
        public Decimal DistToBar { get; set; }
        public Decimal DistToFirstField { get; set; }
        public Decimal ChartSizeX { get; set; }
        public Decimal ChartSizeY { get; set; }

        //For IO devices...
        public string FirstSample { get; set; }
        public string ReadDirection { get; set; }
        public string ReadMode { get; set; }
        public decimal RowsPerPage { get; set; }
        public decimal Pages { get; set; }
    }
}