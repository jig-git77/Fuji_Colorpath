using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class CreateSourceToSourceDeviceLinkProfileRequest
    {
        public IccProfile SourceProfile { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
    }
}
