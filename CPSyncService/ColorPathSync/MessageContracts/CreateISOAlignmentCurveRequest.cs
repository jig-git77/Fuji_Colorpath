using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateISOAlignmentCurveRequest
    {
        [DataMember]
        public double[] PCSData { get; set; }
        [DataMember]
        public double[] DCSData { get; set; }
        [DataMember]
        public double[] DensityData { get; set; }
        [DataMember]
        public bool UseDLP { get; set; }
        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
        [DataMember]
        public string LinearizationTVI25 { get; set; }
        [DataMember]
        public string LinearizationTVI50 { get; set; }
        [DataMember]
        public string LinearizationTVI75 { get; set; }
        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }
    }
}