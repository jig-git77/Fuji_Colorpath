using FujifilmUSA.Sync.Business.ProfilerService;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class CreateDestinationAndDeviceLinkProfileRequest
    {
        public CreateDestinationAndDeviceLinkProfileRequest()
        {
            
        }

       
        public List<double> PcsData { get; set; }
        public List<double> DcsData { get; set; }
        public List<Parameter> DestinationProfileOptions { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public IccProfile SourceProfile { get; set; }
        public PCSDataType PCSDataType { get; set; }
    }
}
