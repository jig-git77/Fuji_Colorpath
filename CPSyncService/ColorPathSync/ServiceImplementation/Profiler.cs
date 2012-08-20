using ColorPathSync.MessageContracts;
using ColorPathSync.ServiceContracts;
using ColorPathSync.Business;

namespace ColorPathSync.ServiceImplementation
{
    public class Profiler : IProfiler
    {
        //To check whether service is running or not.
        public bool Ping()
        {
            return true;
        }

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

        public ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request)
        {
            return ProfilerBusiness.ExtractProfileValues(request);
        }

        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.CreateDestinationAndDeviceLinkProfile(request);
        }
    }
}
