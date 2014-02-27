using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateG7AlignmentCurveRequest
    {
        [DataMember]
        public double[] PCSData { get; set; }
        [DataMember]
        public double[] DCSData { get; set; }    
        [DataMember]
        public bool UseDLP { get; set; }
        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
        [DataMember]
        public List<Parameter> ProfileOptions { get; set; }
        [DataMember]
        public PCSDataType PCSDataType { get; set; }
    }
}