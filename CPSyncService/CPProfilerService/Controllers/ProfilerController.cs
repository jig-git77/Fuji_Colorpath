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
        [HttpGet]
        [Route("api/Profiler/Ping")]
        public bool Ping()
        {
            return true;
        }

        [HttpPost]                
        [Route("api/Profiler/CreateDLP")]
        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.CreateDestinationAndDeviceLinkProfile(request);
            
        }
        [HttpPost]
        [Route("api/Profiler/OptimizeDLP")]
        public OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.OptimizeDeviceLinkProfile(request);
        }
        [HttpPost]
        [Route("api/Profiler/CreateSourceDLP")]
        public CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.CreateSourceToSourceDeviceLinkProfile(request);
        }

    }
}
