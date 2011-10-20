using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class ResponseMessage
{
    [DataMember]
    public string Message { get; set; }
    [DataMember]
    public MessageSeverity Severity { get; set; }
}