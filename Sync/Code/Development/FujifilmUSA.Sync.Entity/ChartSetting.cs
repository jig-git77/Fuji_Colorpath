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
        //public short ChartSettingId { get; set; }
        //public XmlElement Setting { get; set; }
    }
}