using System.Runtime.Serialization;
namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public class ProjectType
    {
        [DataMember]
        public short ProjectTypeId { get; set; }

        [DataMember]
        public string ProjectTypeName { get; set; }
    }
}
