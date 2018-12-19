using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Entity
{
    public class SpotLibrayCMYKExport
    {
        public int OptimizationRunId { get; set; }
        public int LibraryType { get; set; }
        public string FileName { get; set; }
        public string Server { get; set; }
        public string Model { get; set; }
        public string Media { get; set; }
        public string Resolution { get; set; }
        public string Mode { get; set; }
        public string Quality { get; set; }
        public string InkPoolID { get; set; }
        public string Comment { get; set; }
    }
}
