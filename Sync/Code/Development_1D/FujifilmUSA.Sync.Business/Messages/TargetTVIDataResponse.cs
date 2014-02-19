using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Messages
{
    public class TargetTVIDataResponse
    {
        //Only for Sync1D ISO curve
        //public string TVICMY_C { get; set; }
        //public string TVICMY_M { get; set; }
        //public string TVICMY_Y { get; set; }
        //public string TVI_K { get; set; }

        public Dictionary<string, string> TVIOptions {get;set;}
    }
}
