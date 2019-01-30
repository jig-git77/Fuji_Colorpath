using System.Runtime.Serialization;
using Service.Common.MessageContracts;
using FujifilmUSA.ColorOrganizerService.DataContracts.Enums;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetProfileRequest : BaseRequest
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public EProfileType ProfileType { get; set; }
    }
}
