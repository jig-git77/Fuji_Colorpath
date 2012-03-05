

namespace FujifilmUSA.Sync.Entity
{
    public class Report
    {
        public int ReportId { get; set; }
        public string ReportDesc { get; set; }
        public ReportType ReportTypeDetail { get; set; }
        public string IconName { get; set; }
        public string AspxPageName { get; set; }

    }
}
