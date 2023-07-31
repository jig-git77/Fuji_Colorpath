using CPProfilerService.Models.Entity;
using System.Collections.Generic;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    public class CreateDestinationProfileRequest
    {
        public double[] PcsData { get; set; }
        public double[] DcsData { get; set; }
        public List<Parameter> DestinationProfileOptions { get; set; }
        public PCSDataType PCSDataType { get; set; }
    }
}