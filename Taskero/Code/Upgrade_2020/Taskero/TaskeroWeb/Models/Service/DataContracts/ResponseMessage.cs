using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.DataContracts
{
    [DataContract]
    public class ResponseMessage
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public MessageSeverity Severity { get; set; }
    }
}