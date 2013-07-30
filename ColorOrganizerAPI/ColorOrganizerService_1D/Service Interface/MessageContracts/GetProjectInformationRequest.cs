using System.Runtime.Serialization;
using Service.Common.MessageContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetProjectInformationRequest : BaseRequest
    {
        [DataMember]
        public int ProjectId { get; set; }
    }
}
