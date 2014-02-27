using System.Runtime.Serialization;

namespace ColorPathSync.DataContracts
{
    [DataContract]
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

    [DataContract]
    public enum DensityType
    {
        [EnumMember(Value="Status-T")]
        Status_T,
        [EnumMember(Value = "Status-E")]
        Status_E
    }
}