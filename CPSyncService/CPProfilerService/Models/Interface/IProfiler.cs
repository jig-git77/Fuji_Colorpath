using CPProfilerService.Models.Request;
using CPProfilerService.Models.Response;

namespace CPProfilerService.Models.Interface
{
    public interface IProfiler
    {
        bool Ping();
        CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request);
        OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request);
        CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request);
        CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request);
        CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request);
        CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request);
        ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request);
        CreateSpotLibraryResponse CreateSpotLibraryValues(CreateSpotLibraryRequest request);
        OptimizeSpotLibraryResponse OptimizeSpotLibraryValues(OptimizeSpotLibraryRequest request);
        CreateDestinationProfileResponse CreateDestinationProfile(CreateDestinationProfileRequest request);
        CreateDestinationAndDeviceLinkProfileResponse CreateRevoria(CreateRevoriaRequest request);
    }
}