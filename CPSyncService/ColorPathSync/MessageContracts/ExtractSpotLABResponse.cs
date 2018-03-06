using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColorPathSync.MessageContracts
{
    public class ExtractSpotLABResponse : BaseResponse
    {

        public ExtractSpotLABResponse()
        {
            spotPatchDetail = new List<Patch>();
        }
        [DataMember]
        public List<Patch> spotPatchDetail { get; set; }

        public byte[] namedColorProfile { get; set; }       
    }
}