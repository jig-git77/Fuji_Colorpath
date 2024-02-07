using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.MessageContracts
{

    [DataContract]
    public class GetDeviceListRequest
    {
        [DataMember]
        public string DongleName { get; set; }
    }
}