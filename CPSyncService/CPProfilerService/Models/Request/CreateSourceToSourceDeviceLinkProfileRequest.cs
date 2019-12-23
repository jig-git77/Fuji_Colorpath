using CPProfilerService.Models.Entity;
using System.Collections.Generic;

namespace CPProfilerService.Models.Request
{
    public class CreateSourceToSourceDeviceLinkProfileRequest
    {
        public IccProfile SourceProfile { get; set; }       
        public List<Parameter> ProfileOptions { get; set; }
    }
}