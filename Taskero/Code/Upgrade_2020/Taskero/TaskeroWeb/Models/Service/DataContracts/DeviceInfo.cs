using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Service.DataContracts
{
    [DataContract]
    public class DeviceInfo
    {
        [DataMember]
        public int DongleId { get; set; }
        [DataMember]
        public int DeviceId { get; set; }
        [DataMember]
        public string DeviceName { get; set; }
    }
}