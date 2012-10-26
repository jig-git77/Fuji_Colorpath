using Service.Common.MessageContracts;
using System.Runtime.Serialization;
using FujifilmUSA.ColorOrganizerService.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetReferenceChartDataResponse : BaseResponse
    {
        [DataMember]
        public ReferenceChart ReferenceChart { get; set; }
    }
}
