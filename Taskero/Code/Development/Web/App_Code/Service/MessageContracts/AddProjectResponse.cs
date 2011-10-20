using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class AddProjectResponse : ResponseMessage
{
    [DataMember]
    public int ProjectId { get; set; }
}