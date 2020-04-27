using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
   
    public class OptimizeSpotLibraryResponse : BaseResponse
    {
        public OptimizeSpotLibraryResponse()
        {
            spotPatchDetail = new List<Patch>();
        }
        public List<Patch> spotPatchDetail { get; set; }       
        public byte[] namedColorProfile { get; set; }
    }
}