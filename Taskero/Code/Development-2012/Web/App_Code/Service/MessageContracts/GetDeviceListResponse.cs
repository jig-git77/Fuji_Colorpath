using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class GetDeviceListResponse : ResponseMessage
{
    [DataMember]
    public List<DeviceInfo> Devices { get; set; }
}