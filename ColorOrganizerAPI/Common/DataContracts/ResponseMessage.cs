using System.Runtime.Serialization;
namespace Service.Common.DataContracts
{
    [DataContract]
    public class ResponseMessage
    {
        [DataMember]
        public string Message { set; get; }

        [DataMember]
        public MessageSeverity Severity { get; set; }
    }
}
