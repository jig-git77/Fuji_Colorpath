using System;
namespace FujifilmUSA.Sync.Entity.Mock
{
    public class AppErrorLog
    {
        public int AppErrorLogId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ExType { get; set; }
        public string ExMessage { get; set; }
        public string ExStackTracke { get; set; }
        public int UserId { get; set; }
    }
}