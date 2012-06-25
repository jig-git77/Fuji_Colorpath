using System;
using System.Runtime.Serialization;
using Service.Common.MessageContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class StoreTempProjectInformationRequest : BaseRequest
    {
        [DataMember]
        public Guid ProjectInfoId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string Media { get; set; }

        [DataMember]
        public string Colorant { get; set; }

        [DataMember]
        public string RipSettings { get; set; }

        [DataMember]
        public int MeasurementDeviceTypeId { get; set; }

        [DataMember]
        public int ChartId { get; set; }

        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string SilverlightPostUrl { get; set; }
    }
}
