using ColorPathSync.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColorPathSync.MessageContracts
{
    [DataContract]
    public class CreateSpotLibraryResponse : BaseResponse
    {
        public CreateSpotLibraryResponse()
        {
            spotPatchDetail = new List<Patch>();
        }
        [DataMember]
        public List<Patch> spotPatchDetail { get; set; }
        [DataMember]
        public byte[] namedColorProfile { get; set; }       
    }
}