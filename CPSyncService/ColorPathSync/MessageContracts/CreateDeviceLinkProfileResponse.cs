using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateDeviceLinkProfileResponse : BaseResponse
    {
        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
    }
}