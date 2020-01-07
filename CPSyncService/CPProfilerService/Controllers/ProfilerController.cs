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

        [HttpPost]
        [Route("api/Profiler/CreateG7Alignment")]
        public CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request)
        {
            return ProfilerApiBusiness.CreateG7AlignmentCurve(request);
        }

        [HttpPost]
        [Route("api/Profiler/CreateISOAlignment")]
        public CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request)
        {
            return ProfilerApiBusiness.CreateISOAlignmentCurve(request);
        }

        [HttpPost]
        [Route("api/Profiler/CreateSourceProfile")]
        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerApiBusiness.CreateSourceProfile(request);
        }
        [HttpPost]
        [Route("api/Profiler/ExtractProfile")]
        public ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request)
        {
            return ProfilerApiBusiness.ExtractProfileValues(request);
        }
        [HttpPost]
        [Route("api/Profiler/CreateSpotLibrary")]
        public CreateSpotLibraryResponse CreateSpotLibraryValues(CreateSpotLibraryRequest request)
        {
            return ProfilerApiBusiness.CreateSpotLibraryValues(request);
        }
        [HttpPost]
        [Route("api/Profiler/OptimizeSpotLibrary")]
        public OptimizeSpotLibraryResponse OptimizeSpotLibraryValues(OptimizeSpotLibraryRequest request)
        {
            return ProfilerApiBusiness.OptimizeSpotLibraryValues(request);
        }
    }
}
