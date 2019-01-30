using System.Runtime.Serialization;
using Service.Common.MessageContracts;
using System.Collections.Generic;
using FujifilmUSA.ColorOrganizerService.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetDeviceListResponse : BaseResponse
    {
        [DataMember]
        public List<Device> Devices { get; set; }
    }
}
