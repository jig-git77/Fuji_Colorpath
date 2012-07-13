using System.Runtime.Serialization;
namespace FujifilmUSA.ColorOrganizerService.DataContracts.Enums
{
    [DataContract]
    public enum EReferenceChartType
    {
        [EnumMember]
        None=0,

        [EnumMember]
        ProfileChart = 1,

        [EnumMember]
        OptimizationChart = 2
    }
}
