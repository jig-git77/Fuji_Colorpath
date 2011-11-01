using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateSourceProfileResponse : BaseResponse
    {
        [DataMember]
        public IccProfile Profile { get; set; }
    }
}