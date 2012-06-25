using System;

namespace FujifilmUSA.Sync.Entity
{
    public class TempProjectInfo
    {
        public Guid TempProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Media { get; set; }
        public string Colorant { get; set; }
        public string RipSettings { get; set; }
        public int MeasurementDeviceTypeId { get; set; }
        public int ChartId { get; set; }
        public int ProjectId { get; set; }
        public string SilverlightPostUrl { get; set; }
    }
}
