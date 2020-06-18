using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.DataContracts
{
    [DataContract]
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