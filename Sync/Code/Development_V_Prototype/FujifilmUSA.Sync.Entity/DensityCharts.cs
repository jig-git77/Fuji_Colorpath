using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class DensityCharts
    {
        public DensityCharts()
        {
            CyanValues = new MeasurementPatchData();
            MagentaValues = new MeasurementPatchData();
            YellowValues = new MeasurementPatchData();
            RedValues = new MeasurementPatchData();
            GreenValues = new MeasurementPatchData();
            BlueValues = new MeasurementPatchData();
        }

        public MeasurementPatchData CyanValues { get; set; }
        public MeasurementPatchData MagentaValues { get; set; }
        public MeasurementPatchData YellowValues { get; set; }
        public MeasurementPatchData RedValues { get; set; }
        public MeasurementPatchData GreenValues { get; set; }
        public MeasurementPatchData BlueValues { get; set; }
    }
}
