using ColorPathSync.MessageContracts;
using ColorPathSync.ServiceContracts;
using ColorPathSync.Business;

namespace ColorPathSync.ServiceImplementation
{
    public class Profiler : IProfiler
    {
        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerBusiness.CreateSourceProfile(request);
        }

        public CreateDeviceLinkProfileResponse CreateDeviceLinkProfile(CreateDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.CreateDeviceLinkProfile(request);
        }

        public OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.OptimizeDeviceLinkProfile(request);
        }
    }
}
