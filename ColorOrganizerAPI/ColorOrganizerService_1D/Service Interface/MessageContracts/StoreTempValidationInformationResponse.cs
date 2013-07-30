using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.MessageContracts;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class StoreTempValidationInformationResponse : BaseResponse
    {
        [DataMember]
        public Guid ValidationInfoId { get; set; }
    }
}
