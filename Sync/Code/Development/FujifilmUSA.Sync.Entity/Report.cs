using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class Report
    {
        public int ReportID { get; set; }
        public string ReportDesc { get; set; }
        public ReportType ReportTypeDetail { get; set; }
        public string IconName { get; set; }
        public string AspxPageName { get; set; }

    }
}
