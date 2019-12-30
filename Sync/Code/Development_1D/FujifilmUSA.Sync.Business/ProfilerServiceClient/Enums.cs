using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Business.ProfilerServiceClient
{
    public class Enums
    {
        public enum PCSDataType
        {
            [EnumMember]
            Density,
            [EnumMember]
            XYZ,
            [EnumMember]
            Spectral,
            //[EnumMember]
            //LAB      
        }
        public enum DensityType
        {
            [EnumMember(Value = "Status-T")]
            Status_T,
            [EnumMember(Value = "Status-E")]
            Status_E
        }
        public enum MessageSeverity
        {
            [EnumMember]
            None = 0,
            [EnumMember]
            Information = 1,
            [EnumMember]
            Error = 2,
            [EnumMember]
            Warning = 3
        }
    }
}
