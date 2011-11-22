using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class DestinationProfile
    {
        public int DestinationProfileID { get; set; }
        public string DestinationProfileName { get; set; }
        public string Profile { get; set; }
        public Boolean IsUse { get; set; }
    }
}
