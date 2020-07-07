using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    public class OptimizeSpotLibraryResponse : BaseResponse
    {
        public OptimizeSpotLibraryResponse()
        {
            spotPatchDetail = new List<Patch>();
        }
        public List<Patch> spotPatchDetail { get; set; }
        public byte[] namedColorProfile { get; set; }
    }
}
