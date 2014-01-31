using System.Runtime.Serialization;

namespace ColorPathSync.DataContracts
{
    [DataContract]
    public enum PCSDataType
    {
        [EnumMember]
        Remission,
        [EnumMember]
        XYZ,
        [EnumMember]
        LAB,
        [EnumMember]
        Density
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