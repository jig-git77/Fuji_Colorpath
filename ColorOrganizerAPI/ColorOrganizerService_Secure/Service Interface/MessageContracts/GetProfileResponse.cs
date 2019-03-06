using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.MessageContracts;
using System.Runtime.Serialization;
using FujifilmUSA.ColorOrganizerService.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetProfileResponse : BaseResponse
    {
        [DataMember]
        public Profile Profile { get; set; }
    }
}
