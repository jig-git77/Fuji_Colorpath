using CPProfilerService.Models.Entity;

namespace CPProfilerService.Models.Response
{
    public class CreateSourceToSourceDeviceLinkProfileResponse:BaseResponse
    {
        public IccProfile DeviceLinkProfile { get; set; }
    }
}