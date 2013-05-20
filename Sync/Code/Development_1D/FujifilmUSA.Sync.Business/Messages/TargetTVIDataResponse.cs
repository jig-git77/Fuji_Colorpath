using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Messages
{
    public class TargetTVIDataResponse
    {
        //Only for Sync1D ISO curve   
        public string TVI { get; set; }
        public string Threshold { get; set; }        
    }
}
