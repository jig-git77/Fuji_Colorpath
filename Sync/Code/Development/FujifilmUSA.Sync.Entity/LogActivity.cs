using System;

namespace FujifilmUSA.Sync.Entity
{
    public class LogActivity
    {
        public int LogActivityId { get; set; }
        public int UserId { get; set; }
        public LogActivityType ActivityType { get; set; }
        public string IpAddress { get; set; }
        public string Details { get; set; }
        public DateTime ActivityDate { get; set; }
        public LogActivitySystemType SystemType { get; set; }
    }
}
