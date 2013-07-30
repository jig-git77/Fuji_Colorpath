using Service.Common.MessageContracts;
using System.Collections.Generic;
using FujifilmUSA.ColorOrganizerService.DataContracts;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.MessageContracts
{
    [DataContract]
    public class GetReferenceChartListResponse : BaseResponse
    {
        [DataMember]
        public List<ReferenceChart> ReferenceChartList { get; set; }
    }
}
