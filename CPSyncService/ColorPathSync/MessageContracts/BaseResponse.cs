using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public MessageSeverity Severity { get; set; }
    }
}