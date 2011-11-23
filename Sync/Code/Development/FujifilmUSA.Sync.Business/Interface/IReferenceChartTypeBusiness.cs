using System.Collections.Generic;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IReferenceChartTypeBusiness :ITaskBase
    {
        Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId);
    }
}
