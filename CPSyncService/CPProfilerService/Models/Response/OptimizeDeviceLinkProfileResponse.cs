using CPProfilerService.Models.Entity;

namespace CPProfilerService.Models.Response
{
    public class OptimizeDeviceLinkProfileResponse : BaseResponse
    {
        public OptimizeDeviceLinkProfileResponse()
        {

        }
        public IccProfile OptimizedProfile { get; set; }
    }
}