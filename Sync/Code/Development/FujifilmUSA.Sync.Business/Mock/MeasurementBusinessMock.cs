using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementBusinessMock : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public MeasurementDataResponse GetMeasurementData(List<int> measurementIds)
        {
            return new MeasurementDataResponse();
        }

        public int SaveMeasurement(Measurement measurement)
        {
            return 0;
        }

        #endregion
    }
}
