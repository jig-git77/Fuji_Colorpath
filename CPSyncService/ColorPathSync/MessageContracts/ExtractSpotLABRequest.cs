using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class ExtractSpotLABRequest
    {
        [DataMember]
        public SpotIccProfile DestinationProfile { get; set; }
        [DataMember]
        public byte[] TargetProfile { get; set; }
        [DataMember]
        public List<SpotParameter> ProfileOptions { get; set; }

    }
}