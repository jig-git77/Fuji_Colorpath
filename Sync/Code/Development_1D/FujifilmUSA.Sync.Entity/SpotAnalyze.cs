using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class SpotAnalyze
    {
        public SpotAnalyze()
        {
            patchDetails = new List<Dictionary<string, object>>();
        }

        public string maxDeltaE { get; set; }
        public string minDeltaE { get; set; }
        public string avgDeltaE { get; set; }
        public List<Dictionary<string, object>> patchDetails { get; set; }

    }
}
