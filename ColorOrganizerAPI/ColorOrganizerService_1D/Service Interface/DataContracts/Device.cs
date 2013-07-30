using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public class Device
    {
        [DataMember]
        public int DeviceId { get; set; }

        [DataMember]
        public string DeviceName { get; set; }
    }
}
