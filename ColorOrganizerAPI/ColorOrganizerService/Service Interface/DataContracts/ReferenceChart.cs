using System.Runtime.Serialization;

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
        public byte[] ChartPreview { get; set; }
        
        [DataMember]
        public byte[] OriginalChart { get; set; }

        [DataMember]
        public EMeasurementDevice MeasurementDevice { get; set; }
    }
}
