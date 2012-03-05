using System;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartType
    {
        public ReferenceChartType()
        {
            ChartPatches = new List<ReferenceChart>();
        }

        public int ReferenceChartTypeId { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public int TotalPatches { get; set; }
        public int MeasurementDeviceTypeId { get; set; }
        public Boolean IsUsed { get; set; }
        public List<ReferenceChart> ChartPatches { get; set; }
    }
}
