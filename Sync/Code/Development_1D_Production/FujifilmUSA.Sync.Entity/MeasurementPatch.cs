using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatch
    {
        public int MeasurementPatchId { get; set; }
        public int MeasurementId { get; set; }
        public string SampleName { get; set; }
        public decimal CiexyzX { get; set; }
        public decimal CiexyzY { get; set; }
        public decimal CiexyzZ { get; set; }
        public decimal CielabL { get; set; }
        public decimal CielabA { get; set; }
        public decimal CielabB { get; set; }
        public decimal? Δe { get; set; }
        public decimal? Δe94 { get; set; }
        public decimal? Δe2000 { get; set; }
        public string HtmlColor { get; set; }
        public Density Density { get; set; }
        public Spectral Spectral { get; set; }

        //public List<MeasuredColorChar> MeasuredColorChars { get; set; }
        //public Measurement Measurement { get; set; }
    }
}