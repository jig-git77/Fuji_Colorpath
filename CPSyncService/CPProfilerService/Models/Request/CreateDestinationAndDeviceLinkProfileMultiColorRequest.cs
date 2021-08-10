using CPProfilerService.Models.Entity;
using System.Collections.Generic;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    public class CreateDestinationAndDeviceLinkProfileMultiColorRequest
    {
        public CreateDestinationAndDeviceLinkProfileMultiColorRequest()
        {

        }


        public double[] PcsData { get; set; }
        public double[] DcsData { get; set; }
        public List<Parameter> DestinationProfileOptions { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public IccProfile SourceProfile { get; set; }
        public PCSDataType PCSDataType { get; set; }        
        public int TotalNoColors { get; set; }
    }
}