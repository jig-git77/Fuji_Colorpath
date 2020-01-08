using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FujifilmUSA.Sync.Business.ProfilerServiceClient.Enums;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class CreateSourceProfileRequest
    {

        public List<double> PCSData { get; set; }
        public List<double> DCSData { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public PCSDataType PCSDataType { get; set; }
        public IccProfile SourceProfile { get; set; }
    }
}
