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
        public int measurementID { get; set; }

        public string maxDeltaE { get; set; }
        public string minDeltaE { get; set; }
        public string avgDeltaE { get; set; }

        public string minDeltaETP { get; set; }
        public string maxDeltaETP { get; set; }        
        public string avgDeltaETP { get; set; }

        public string minDeltaEMT { get; set; }
        public string maxDeltaEMT { get; set; }        
        public string avgDeltaEMT { get; set; }

        public string minDeltaEMP { get; set; }
        public string maxDeltaEMP { get; set; }        
        public string avgDeltaEMP { get; set; }

        public List<Dictionary<string, object>> patchDetails { get; set; }

    }
}
