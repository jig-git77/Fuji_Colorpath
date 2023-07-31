using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
    public class CreateDestinationProfileResponse : BaseResponse
    {
        public IccProfile DestinationProfile { get; set; }
    }
}