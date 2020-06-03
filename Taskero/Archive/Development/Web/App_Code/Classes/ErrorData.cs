using System;
using System.Collections.Generic;

using System.Web;
using System.Runtime.Serialization;

namespace MeasurementPlugInSvc
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