using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[DataContract]
public class GetProjectDetailRequest
{
	[DataMember]
	public int ProjectID { get; set; }
}