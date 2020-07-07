using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
    public class CreateDestinationAndDeviceLinkProfileResponse:BaseResponse
    {
        
        public IccProfile DestinationProfile { get; set; }       
        public IccProfile DeviceLinkProfile { get; set; }
    }
}