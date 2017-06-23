using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSyncV
{
    public class TempValidationInfo
    {
        public Guid TempValidationId { get; set; }
        public string JobName { get; set; }
        public string Customer { get; set; }
        public string PageOrSurface { get; set; }
        public string JDFJobId { get; set; }
        public int ProjectId { get; set; }
        public string SilverlightPostUrl { get; set; }
    }
}
