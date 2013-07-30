using System.Runtime.Serialization;
using Service.Common.MessageContracts;
using FujifilmUSA.ColorOrganizerService.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetProjectInformationResponse : BaseResponse
    {
        [DataMember]
        public ProjectInformation ProjectInformation { get; set; }
    }
}
