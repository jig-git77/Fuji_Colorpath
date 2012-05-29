using System.Runtime.Serialization;
namespace Service.Common.DataContracts
{
    [DataContract(Name = "MessageSeverity")]
    public enum MessageSeverity
    {
        [EnumMember]
        None=0,
        
        [EnumMember]
        Exception=1,
        
        [EnumMember]
        Information=2,
        
        [EnumMember]
        Warning=3
    }
}