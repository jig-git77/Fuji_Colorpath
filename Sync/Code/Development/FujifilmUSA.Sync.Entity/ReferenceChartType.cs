
using System;
namespace FujifilmUSA.Sync.Entity
{
    public class ReferenceChartType
    {
        public int ReferenceChartTypeId { get; set; }
        public string ChartName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }
        public int MeasurementDeviceTypeId { get; set; }
        public Boolean IsUsed { get; set; }
    }
}
