using System.Collections.Generic;
using System.Runtime.Serialization;
using TaskeroWeb.Models.Service.DataContracts;

namespace TaskeroWeb.Models.Service.MessageContracts
{

    [DataContract]
    public class GetDeviceListResponse : ResponseMessage
    {
        [DataMember]
        public List<DeviceInfo> Devices { get; set; }
    }
}