using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatchData
    {
        public MeasurementPatchData()
        {
            CielabA = new List<decimal>();
            CielabB = new List<decimal>();
            Density = new List<string>();

            ThresholdA = 8;
            ThresholdB = 8;
        }

        public int PatchTypeId { get; set; }
        public List<decimal> CielabA { get; set; }
        public List<decimal> CielabB { get; set; }
        public List<string> Density { get; set; }
        public string XPosition { get; set; }
        public string YPosition { get; set; }

        public decimal TargetA { get; set; }
        public decimal TargetB { get; set; }
        public decimal ThresholdA { get; set; }
        public decimal ThresholdB { get; set; }
    }
}
