using CPProfilerService.Business;
using CPProfilerService.Models.Entity;
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
        [CustomAuthentication]
        public bool Ping()
        {
            return true;
        }

        [HttpPost]                
        [Route("api/Profiler/CreateDLP")]
        [CustomAuthentication]
        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.CreateDestinationAndDeviceLinkProfile(request);
            
        }
        [HttpPost]
        [Route("api/Profiler/OptimizeDLP")]
        [CustomAuthentication]
        public OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.OptimizeDeviceLinkProfile(request);
        }
        [HttpPost]
        [Route("api/Profiler/CreateSourceDLP")]
        [CustomAuthentication]
        public CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request)
        {
            return ProfilerApiBusiness.CreateSourceToSourceDeviceLinkProfile(request);
        }

        [HttpPost]
        [Route("api/Profiler/CreateG7Alignment")]
        [CustomAuthentication]
        public CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request)
        {
            return ProfilerApiBusiness.CreateG7AlignmentCurve(request);
        }

        [HttpPost]
        [Route("api/Profiler/CreateISOAlignment")]
        [CustomAuthentication]
        public CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request)
        {
            return ProfilerApiBusiness.CreateISOAlignmentCurve(request);
        }

        [HttpPost]
        [Route("api/Profiler/CreateSourceProfile")]
        [CustomAuthentication]
        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerApiBusiness.CreateSourceProfile(request);
        }
        [HttpPost]
        [Route("api/Profiler/ExtractProfile")]
        [CustomAuthentication]
        public ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request)
        {
            return ProfilerApiBusiness.ExtractProfileValues(request);
        }
        [HttpPost]
        [Route("api/Profiler/CreateSpotLibrary")]
        [CustomAuthentication]
        public CreateSpotLibraryResponse CreateSpotLibraryValues(CreateSpotLibraryRequest request)
        {
            return ProfilerApiBusiness.CreateSpotLibraryValues(request);
        }
        [HttpPost]
        [Route("api/Profiler/OptimizeSpotLibrary")]
        [CustomAuthentication]
        public OptimizeSpotLibraryResponse OptimizeSpotLibraryValues(OptimizeSpotLibraryRequest request)
        {
            return ProfilerApiBusiness.OptimizeSpotLibraryValues(request);
        }


        [HttpPost]
        [Route("api/Profiler/CreateDLPMultiColor")]
        [CustomAuthentication]
        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfileMultiColor(CreateDestinationAndDeviceLinkProfileMultiColorRequest request)
        {
            return ProfilerApiBusiness.CreateDestinationAndDeviceLinkMultiColorProfile(request);

        }
    }
}
