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
        DestinationPRofile = 2,
        
        [EnumMember]
        DeviceLinkProfile = 3
    }
}
