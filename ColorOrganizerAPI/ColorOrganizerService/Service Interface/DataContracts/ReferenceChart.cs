using System.Runtime.Serialization;
using FujifilmUSA.ColorOrganizerService.DataContracts.Enums;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public class ReferenceChart
    {
        [DataMember]
        public int ChartId { get; set; }

        [DataMember]
        public string ChartName { get; set; }

        [DataMember]
        public int NumOfRows { get; set; }

        [DataMember]
        public int NumOfCols { get; set; }

        [DataMember]
        public int TotalPatches { get; set; }

        [DataMember]
        public ProjectType ProjectType { get; set; }

        [DataMember]
        public byte[] ChartPreview { get; set; }

        [DataMember]
        public byte[] OriginalChart { get; set; }

        [DataMember]
        public EMeasurementDevice MeasurementDevice { get; set; }

        [DataMember]
        public EReferenceChartType ChartType { get; set; }
    }
}
