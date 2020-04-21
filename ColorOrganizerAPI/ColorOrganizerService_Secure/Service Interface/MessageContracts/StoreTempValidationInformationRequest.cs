using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.MessageContracts;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class StoreTempValidationInformationRequest : BaseRequest
    {
        [DataMember]
        public Guid TempValidationId { get; set; }

        [DataMember]
        public string JobName { get; set; }

        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string PageOrSurface { get; set; }

        [DataMember]
        public string JDFJobId { get; set; }

        [DataMember]
        public string SilverlightPostUrl { get; set; }

        [DataMember]
        public int ProjectId { get; set; }
    }
}
