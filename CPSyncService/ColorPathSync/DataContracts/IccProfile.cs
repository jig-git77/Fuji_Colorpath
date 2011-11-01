using System.Runtime.Serialization;

namespace ColorPathSync.DataContracts
{
    [DataContract]
    public class IccProfile
    {
        [DataMember]
        public byte[] Profile { get; set; }
    }
}