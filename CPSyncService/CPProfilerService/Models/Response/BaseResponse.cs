using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Response
{
    public class BaseResponse
    {

        public string Message { get; set; }       
        public MessageSeverity Severity { get; set; }
    }
}