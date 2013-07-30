using System;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public class ProjectInformation
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public int OptimizeRunCount { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public string Media { get; set; }

        [DataMember]
        public string Colorant { get; set; }

        [DataMember]
        public string RipSettings { get; set; }

        [DataMember]
        public int DeviceId { get; set; }

        [DataMember]
        public int ChartId { get; set; }

        [DataMember]
        public int ValidationChartId { get; set; }

        [DataMember]
        public ProjectStatus Status { get; set; }

        [DataMember]
        public int SourceProfileId { get; set; }

        [DataMember]
        public string SourceProfileName { get; set; }
    }
}
