using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using TaskeroWeb.Models.Service.DataContracts;

namespace TaskeroWeb.Models.Service.MessageContracts
{
    [DataContract]
    public class GetProjectsResponse : ResponseMessage
    {
        [DataMember]
        public List<ProjectInfo> Projects { get; set; }
    }
}