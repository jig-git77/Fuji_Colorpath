using System.Runtime.Serialization;

namespace Service.Common.FaultContracts
{
    [DataContract]
    public enum ErrorCode
    {
        [EnumMember]
        None = 0,
        
        [EnumMember]
        DataNotFound = 1,

        [EnumMember]
        UserNotFound = 2,

        [EnumMember]
        InValidCredential = 3,

        [EnumMember]
        BadRequest = 4,

        [EnumMember]
        UnKnown = 5,

        [EnumMember]
        Internal = 6
    }
}
