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

        public CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.CreateDestinationAndDeviceLinkProfile(request);
        }

        public OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.OptimizeDeviceLinkProfile(request);
        }

        public CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request)
        {
            return ProfilerBusiness.CreateSourceToSourceDeviceLinkProfile(request);
        }

        public CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request)
        {
            return ProfilerBusiness.CreateG7AlignmentCurve(request);
        }

        public CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request)
        {
            return ProfilerBusiness.CreateISOAlignmentCurve(request);
        }

        public CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request)
        {
            return ProfilerBusiness.CreateSourceProfile(request);
        }

        public ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request)
        {
            return ProfilerBusiness.ExtractProfileValues(request);
        }

        public ExtractSpotLABResponse GetSpotLAB(ExtractSpotLABRequest request)
        {
            return ExtractSpotLABBusiness.ExtractSpotLABValues(request);
        }


        ////Implementation for the ISO and G7 for Alignment Curve (Old Code)
        //public Profile CreateCurve(Curve request)
        //{
        //    return CreateCurveProfileBusiness.CreateCurve(request);
        //}

        //public Profile CreateISOCurve(Curve request)
        //{
        //    return CreateISOCurveProfileBusiness.CreateISOCurve(request);
        //}

    }
}
