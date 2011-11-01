using System.Collections.Generic;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateDeviceLinkProfileRequest
    {
        [DataMember]
        public IccProfile SourceProfile { get; set; }
        [DataMember]
        public IccProfile DestinationProfile { get; set; }
        [DataMember]
        public List<Parameter> Options { get; set; }
    }
}