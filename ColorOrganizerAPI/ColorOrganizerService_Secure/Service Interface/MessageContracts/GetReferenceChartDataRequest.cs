using Service.Common.MessageContracts;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetReferenceChartDataRequest : BaseRequest
    {
        [DataMember]
        public int ChartId { get; set; }
    }
}
