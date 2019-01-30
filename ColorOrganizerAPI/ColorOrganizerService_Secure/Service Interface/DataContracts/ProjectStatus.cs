using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public enum ProjectStatus
    {
        [EnumMember]
        Open = 1,

        [EnumMember]
        Closed = 2,

        [EnumMember]
        Certify = 3
    }
}
