using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Request
{
  
    public class OptimizeSpotLibraryRequest
    {
       
        public SpotIccProfile DestinationProfile { get; set; }        
        public SpotIccProfile previousNamedColorProfile { get; set; }       
        public byte[] TargetProfile { get; set; }       
        public List<SpotParameter> ProfileOptions { get; set; }
    }
}