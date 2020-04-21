using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{
    public class UserReferenceChart
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int OptimizationRunID { get; set; }
        public User User { get; set; }
        public ReferenceChartData ReferenceChartData { get; set; }
    }
}
