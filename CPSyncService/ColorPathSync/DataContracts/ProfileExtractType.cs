using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ColorPathSync.DataContracts
{
    [DataContract]
    public enum ProfileExtractType
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Xyz = 1,

        [EnumMember]
        Lab = 2
    }
}