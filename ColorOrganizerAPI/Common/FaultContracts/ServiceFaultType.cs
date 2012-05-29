using System.Runtime.Serialization;

namespace Service.Common.FaultContracts
{
    [DataContract]
    public enum ServiceFaultType
    {
        [EnumMember]
        ConnectionError = 0,

        [EnumMember]
        DataBase = 1,

        [EnumMember]
        Application = 2,

        [EnumMember]
        RequestError = 3,

        [EnumMember]
        Unknown = 4
    }
}