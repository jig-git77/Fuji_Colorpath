using System.Runtime.Serialization;

namespace Service.Common.MessageContracts
{
    [DataContract]
    public class BaseRequest
    {
        public bool IsAuthenticationRequired { get; set; }

        [DataMember]
        public string SecurityToken { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserPassword { get; set; }
    }
}
