using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateDestinationAndDeviceLinkProfileRequest
    {
        [DataMember]
        public double[] PcsData { get; set; }

        [DataMember]
        public double[] DcsData { get; set; }

        [DataMember]
        public List<Parameter> DestinationProfileOptions { get; set; }

        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }

        [DataMember]
        public IccProfile SourceProfile { get; set; }
    }
}