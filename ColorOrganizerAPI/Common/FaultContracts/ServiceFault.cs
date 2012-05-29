using System.Runtime.Serialization;

namespace Service.Common.FaultContracts
{
    [DataContract]
    public class ServiceFault
    {
        [DataMember]
        public string FaultMessage { get; set; }

        [DataMember]
        public ErrorCode FaultErrorCode { get; set; }

        [DataMember]
        public string FaultDetails { get; set; }

        [DataMember]
        public ServiceFaultType FaultType { get; set; }

        [DataMember]
        public string FaultSource { get; set; }
    }
}