using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementBusiness : ITaskBase
    {
        MeasurementDataResponse GetMeasurementData(List<int> measurementIds);
    }
}
