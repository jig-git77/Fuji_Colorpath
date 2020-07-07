using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    public class CreateSourceToSourceDeviceLinkProfileResponse : BaseResponse
    {
        public IccProfile DeviceLinkProfile { get; set; }
    }
}
