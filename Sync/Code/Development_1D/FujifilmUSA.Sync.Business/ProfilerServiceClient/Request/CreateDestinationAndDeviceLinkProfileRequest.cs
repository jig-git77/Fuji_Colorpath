using System.Collections.Generic;
using static FujifilmUSA.Sync.Business.ProfilerServiceClient.Enums;

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
        public List<Parameter> ChannelOptions { get; set; }
        public int TotalNoColors { get; set; }
    }
}
