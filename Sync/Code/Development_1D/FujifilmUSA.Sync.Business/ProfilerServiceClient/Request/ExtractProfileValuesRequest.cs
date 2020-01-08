using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FujifilmUSA.Sync.Business.ProfilerServiceClient.Enums;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class ExtractProfileValuesRequest
    {

        public ProfileExtractType ExtractType { get; set; }
        public IccProfile Profile { get; set; }
        public List<Parameter> ProfileOptions { get; set; }
        public List<double> C_X { get; set; }
        public List<double> M_Y { get; set; }
        public List<double> Y_Z { get; set; }
        public List<double> K { get; set; }
    }
}
