using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.MessageContracts
{
    [DataContract]
    public class GetProjectsRequest
    {
        [DataMember]
        public string DongleName { get; set; }

        [DataMember]
        public int DeviceId { get; set; }
    }
}