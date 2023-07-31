using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FujifilmUSA.Sync.Business.ProfilerServiceClient.Enums;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class CreateDestinationProfileRequest
    { 
        public List<double> PcsData { get; set; }
        public List<double> DcsData { get; set; }
        public List<Parameter> DestinationProfileOptions { get; set; }
        public PCSDataType PCSDataType { get; set; }
    }
}
