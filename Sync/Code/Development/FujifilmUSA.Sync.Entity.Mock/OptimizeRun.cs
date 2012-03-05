using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Entity.Mock
{
    public class OptimizeRun
    {
        public int OptimizeRunId { get; set; }
        public string RunName { get; set; }
        public int ProjectId { get; set; }
        public byte Status { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public int? MasterRunId { get; set; }
        public bool? IsUsed { get; set; }
        public List<Measurement> Measurements { get; set; }
        public Project Project { get; set; }
    }
}