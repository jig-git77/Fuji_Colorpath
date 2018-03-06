using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColorPathSync.MessageContracts
{
    public class ExtractSpotLABRequest
    {
        [DataMember]
        public IccProfile DestinationProfile { get; set; }
        [DataMember]
        public byte[] TargetProfile { get; set; }
        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }

    }
}