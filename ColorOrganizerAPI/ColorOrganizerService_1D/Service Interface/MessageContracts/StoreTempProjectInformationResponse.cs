using System.Runtime.Serialization;
using Service.Common.MessageContracts;
using System;
namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class StoreTempProjectInformationResponse : BaseResponse
    {
        [DataMember]
        public Guid ProjectInfoId { get; set; }
    }
}
