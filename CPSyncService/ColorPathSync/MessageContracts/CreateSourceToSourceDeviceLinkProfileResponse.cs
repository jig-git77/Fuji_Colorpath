using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateSourceToSourceDeviceLinkProfileResponse : BaseResponse
    {
        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
    }
}