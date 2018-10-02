using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class DeviceInfo
{
    [DataMember]
    public int DongleId { get; set; }
    [DataMember]
    public int DeviceId { get; set; }
    [DataMember]
    public string DeviceName { get; set; }
}