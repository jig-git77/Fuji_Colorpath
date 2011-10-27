using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class GetProjectDetailResponse : ResponseMessage
{
    [DataMember]
    public byte[] Profile { get; set; }
}