using ColorPathSync.MessageContracts;
using ColorPathSync.ServiceContracts;

namespace ColorPathSync.ServiceImplementation
{
    public class Profiler : IProfiler
    {
        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerBusiness.CreateSourceProfile(request);
        }
    }
}
