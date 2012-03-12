using FujifilmUSA.Sync.Business.Messages;

namespace FujifilmUSA.Sync.Business.Interface
{
    interface IMeasurementBusiness
    {
        MeasurementDataResponse GetMeasurementData(int[] measurementIds);
    }
}
