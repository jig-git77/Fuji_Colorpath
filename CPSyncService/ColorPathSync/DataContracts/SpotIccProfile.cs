using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ColorPathSync.DataContracts
{
    [DataContract]
    public class SpotIccProfile
    {
        [DataMember]
        public byte[] ProfileData { get; set; }
    }
}