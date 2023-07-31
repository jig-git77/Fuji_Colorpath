using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    internal class CreateDestinationProfileResponse : BaseResponse
    {
        public IccProfile DestinationProfile { get; set; }
    }
}
