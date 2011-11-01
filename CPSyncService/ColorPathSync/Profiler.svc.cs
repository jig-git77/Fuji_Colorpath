using ColorPathSync.MessageContracts;

namespace ColorPathSync
{
    public class Profiler : IProfiler
    {
        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerBusiness.CreateSourceProfile(request);
        }
    }
}
