using System.Collections.Generic;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateSourceProfileRequest
    {
        [DataMember]
        public List<double> PCSData { get; set; }
        [DataMember]
        public List<double> DCSData { get; set; }
        [DataMember]
        public List<Parameter> Options { get; set; }
    }
}