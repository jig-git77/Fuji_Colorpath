using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IReferenceChartBusiness :ITaskBase
    {
        Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId);

        ReferenceChartData GetReferenceChartData(int projectId);
    }
}
