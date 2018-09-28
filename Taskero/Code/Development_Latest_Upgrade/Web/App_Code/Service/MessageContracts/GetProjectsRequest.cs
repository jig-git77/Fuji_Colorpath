using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class GetProjectsRequest
{
    [DataMember]
    public string DongleName { get; set; }
    
    [DataMember]
    public int DeviceId { get; set; }
}