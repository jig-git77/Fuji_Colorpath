using System.Runtime.Serialization;
using TaskeroWeb.Models.Service.DataContracts;

namespace TaskeroWeb.Models.Service.MessageContracts
{
    [DataContract]
    public class GetProjectDetailResponse : ResponseMessage
    {
        [DataMember]
        public byte[] Profile { get; set; }
    }
}