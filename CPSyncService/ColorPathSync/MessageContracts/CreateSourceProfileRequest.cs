using System.Collections.Generic;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateSourceProfileRequest
    {
        [DataMember]
        public double[] PCSData { get; set; }
        [DataMember]
        public double[] DCSData { get; set; }
        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }
    }
}