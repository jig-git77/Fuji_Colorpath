using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IReferenceChartBusiness : ITaskBase
    {
        ReferenceChartData GetReferenceChartData(int projectId);
        ReferenceChartData GetReferenceChartDataByTypeId(int referenceChartTypeId, string serialNo);
    }
}
