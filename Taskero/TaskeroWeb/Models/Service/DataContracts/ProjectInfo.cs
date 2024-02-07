using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.DataContracts
{

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
}