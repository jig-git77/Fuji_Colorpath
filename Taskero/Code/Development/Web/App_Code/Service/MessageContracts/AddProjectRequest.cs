using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class AddProjectRequest
{
    [DataMember]
    public string DongleName { get; set; }
    [DataMember]
    public short DeviceId { get; set; }
    [DataMember]
    public string ProjectName { get; set; }
    [DataMember]
    public int ProjectType { get; set; }
    [DataMember]
    public int OutputType { get; set; }
}