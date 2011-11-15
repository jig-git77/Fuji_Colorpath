using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public enum MessageSeverity
{
    [EnumMember]
    None = 0,
    [EnumMember]
    Information = 1,
    [EnumMember]
    Error = 2,
    [EnumMember]
    Warning = 3
}