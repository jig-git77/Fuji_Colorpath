using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts.Enums
{
    [DataContract]
    public enum EProfileType
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        SourceProfile = 1,

        [EnumMember]
        DestinationProfile = 2,

        [EnumMember]
        DeviceLinkProfile = 3,

        [EnumMember]
        OptimizeDlp = 4
    }
}
