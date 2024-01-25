using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TaskeroWeb.Models.Service.MessageContracts
{
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
}