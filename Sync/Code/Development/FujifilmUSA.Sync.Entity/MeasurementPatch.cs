using System;
namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatch
    {
        public int MeasurementPatchId { get; set; }
        public int MeasurementId { get; set; }
        public string SampleName { get; set; }
        public short SampleId { get; set; }
        public decimal DensityV { get; set; }
        public decimal DensityC { get; set; }
        public decimal DensityM { get; set; }
        public decimal DensityY { get; set; }
        public decimal CiexyzX { get; set; }
        public decimal CiexyzY { get; set; }
        public decimal CiexyzZ { get; set; }
        public decimal CielabL { get; set; }
        public decimal CielabA { get; set; }
        public decimal CielabB { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
    }
}
