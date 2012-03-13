using ColorPathSync.DataContracts;
using System.Runtime.Serialization;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateDestinationAndDeviceLinkProfileResponse : BaseResponse
    {
        [DataMember]
        public IccProfile DestinationProfile { get; set; }

        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
    }
}