using Service.Common.MessageContracts;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class CheckUserCredentialsResponse : BaseResponse
    {
        [DataMember]
        public bool IsValid { get; set; }
    }
}
