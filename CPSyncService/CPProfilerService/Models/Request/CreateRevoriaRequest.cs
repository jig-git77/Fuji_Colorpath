using CPProfilerService.Models.Entity;
using System.Collections.Generic;
using static CPProfilerService.Models.Entity.Enums;
namespace CPProfilerService.Models.Request
{
    public class CreateRevoriaRequest
    {
        public CreateRevoriaRequest()
        {

        }


        public double[] PcsData { get; set; }
        public double[] DcsData { get; set; }
        public List<Parameter> DestinationProfileOptions { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public IccProfile SourceProfile { get; set; }
        public PCSDataType PCSDataType { get; set; }
    }
}