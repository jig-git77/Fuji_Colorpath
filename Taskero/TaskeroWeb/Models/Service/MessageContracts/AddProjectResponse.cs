using System.Runtime.Serialization;
using TaskeroWeb.Models.Service.DataContracts;

namespace TaskeroWeb.Models.Service.MessageContracts
{

    [DataContract]
    public class AddProjectResponse : ResponseMessage
    {
        [DataMember]
        public int ProjectId { get; set; }
    }
}