using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Classes
{
    [DataContract]
    public class ErrorData
    {

        public ErrorData(string reason, string detailedInformation)
        {
            Reason = reason;
            DetailedInformation = detailedInformation;
        }

        [DataMember]
        public string Reason { get; private set; }

        [DataMember]
        public string DetailedInformation { get; private set; }

    }
}