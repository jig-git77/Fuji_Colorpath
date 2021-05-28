using System.Collections.Generic;
using static FujifilmUSA.Sync.Business.ProfilerServiceClient.Enums;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class OptimizeDeviceLinkProfileRequest
    {

        public IccProfile SourceProfile { get; set; }
        public IccProfile DestinationProfile { get; set; }
        public IccProfile DeviceLinkProfile { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public List<double> PCSData { get; set; }
        public List<double> DCSData { get; set; }
        public PCSDataType PCSDataType { get; set; }
       
    }
}
