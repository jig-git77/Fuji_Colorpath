using System;

namespace FujifilmUSA.Sync.Entity
{
    public class DLP
    {
        public int DLPId { get; set; }
        public string DLPName { get; set; }
        public string Profile { get; set; }
        public Boolean IsUse { get; set; }
        public SourceProfile DLPSourceProfile { get; set; }
        public DestinationProfile DLPDestinationProfile { get; set; }
        
    }
}
