using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Response
{
    public class ExtractProfileValuesResponse : BaseResponse
    {
        public double[] C_X { get; set; }
        public double[] M_Y { get; set; }
        public double[] Y_Z { get; set; }
        public double[] K { get; set; }
    }
}
