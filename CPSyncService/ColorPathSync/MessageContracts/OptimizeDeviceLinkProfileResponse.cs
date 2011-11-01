using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class OptimizeDeviceLinkProfileResponse : BaseResponse
    {
        [DataMember]
        public IccProfile OptimizedProfile { get; set; }
    }
}