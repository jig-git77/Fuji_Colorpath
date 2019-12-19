using CPProfilerService.Models.Request;
using CPProfilerService.Models.Response;

namespace CPProfilerService.Models.Interface
{
    public interface IProfiler
    {
        bool Ping();
        CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request);
    }
}
