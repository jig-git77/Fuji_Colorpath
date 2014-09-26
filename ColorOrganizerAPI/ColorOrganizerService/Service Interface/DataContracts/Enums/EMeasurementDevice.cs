using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public enum EMeasurementDevice
    {
        [EnumMember]
        None=0,

        [EnumMember]
        I1Pro=1,

        [EnumMember]
        I1Isis=2,

        [EnumMember]
        I1IO=3,

        [EnumMember]
        LFP=6
    }
}
