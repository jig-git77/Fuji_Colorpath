using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient.Request
{
    public class CreateSpotLibraryRequest
    {

        public SpotIccProfile DestinationProfile { get; set; }
        public byte[] TargetProfile { get; set; }
        public List<SpotParameter> ProfileOptions { get; set; }

    }
}
