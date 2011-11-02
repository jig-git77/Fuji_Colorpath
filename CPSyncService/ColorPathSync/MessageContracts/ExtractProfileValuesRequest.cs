using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class ExtractProfileValuesRequest
    {
        [DataMember]
        public ProfileExtractType ExtractType { get; set; }
        [DataMember]
        public IccProfile Profile { get; set; }
        [DataMember]
        public List<Parameter> Options { get; set; }
        [DataMember]
        public double[] C_X{ get; set; }
        [DataMember]
        public double[] M_Y { get; set; }
        [DataMember]
        public double[] Y_Z { get; set; }
        [DataMember]
        public double[] K { get; set; }
    }
}