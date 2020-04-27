using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    public class CreateSourceProfileResponse : BaseResponse
    {
        public IccProfile Profile { get; set; }
    }
}
