using CPProfilerService.Business;
using CPProfilerService.Models.Interface;
using CPProfilerService.Models.Request;
using CPProfilerService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CPProfilerService.Controllers
{
    public class ProfilerController : ApiController, IProfiler
    {
        public ProfilerController()
        {

        }
        public bool Ping()
        {
            return true;
        }
        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.CreateDestinationAndDeviceLinkProfile(request);
            
        }

        
    }
}
