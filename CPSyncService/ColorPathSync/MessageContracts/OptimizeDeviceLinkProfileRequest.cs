using System.Collections.Generic;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class OptimizeDeviceLinkProfileRequest
    {
        [DataMember]
        public IccProfile SourceProfile { get; set; }
        [DataMember]
        public IccProfile DestinationProfile { get; set; }
        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }
        [DataMember]
        public double[] PCSData { get; set; }
        [DataMember]
        public double[] DCSData { get; set; }
    }
}