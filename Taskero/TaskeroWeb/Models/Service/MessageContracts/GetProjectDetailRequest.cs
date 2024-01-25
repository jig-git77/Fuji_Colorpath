using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.MessageContracts
{
    [DataContract]
    public class GetProjectDetailRequest
    {
        [DataMember]
        public int ProjectID { get; set; }
    }
}