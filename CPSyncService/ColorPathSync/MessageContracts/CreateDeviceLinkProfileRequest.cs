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
        public List<Parameter> DlpOptions { get; set; }
    }
}