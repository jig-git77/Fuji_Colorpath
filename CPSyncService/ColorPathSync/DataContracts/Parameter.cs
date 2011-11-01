using System.Runtime.Serialization;

namespace ColorPathSync.DataContracts
{
    [DataContract]
    public class Parameter
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Value { get; set; }
    }
}