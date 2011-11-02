using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class ExtractProfileValuesResponse : BaseResponse
    {
        [DataMember]
        public double[] C_X { get; set; }
        [DataMember]
        public double[] M_Y { get; set; }
        [DataMember]
        public double[] Y_Z { get; set; }
        [DataMember]
        public double[] K { get; set; }
    }
}