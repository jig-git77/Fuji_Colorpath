using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class ProjectInfo
{
    [DataMember]
    public int ProjectId { get; set; }
    
    [DataMember]
    public string ProjectName { get; set; }

    [DataMember]
    public int DeviceId { get; set; }

    [DataMember]
    public int ΔEType { get; set; }
}