using FujifilmUSA.Sync.Business.ProfilerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    public class BaseResponse
    {

        public string Message { get; set; }
        public MessageSeverity Severity { get; set; }
    }
}
