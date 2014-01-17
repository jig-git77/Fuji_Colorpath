using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ColorPathSync.DataContracts;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateISOAlignmentCurveResponse : BaseResponse
    {

        public CreateISOAlignmentCurveResponse()
        {
            Cyan = new double[101];
            Magenta = new double[101];
            Yellow = new double[101];
            Key = new double[101];
            DeviceLinkProfile = new IccProfile();
        }

        [DataMember]
        public IccProfile DeviceLinkProfile { get; set; }
        [DataMember]
        public double[] Cyan { get; set; }
        [DataMember]
        public double[] Magenta { get; set; }
        [DataMember]
        public double[] Yellow { get; set; }
        [DataMember]
        public double[] Key { get; set; }
    }
}