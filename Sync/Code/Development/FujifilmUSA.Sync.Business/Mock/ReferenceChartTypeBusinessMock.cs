using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ReferenceChartTypeBusinessMock : IReferenceChartTypeBusiness
    {
        #region IReferenceChartTypeBusiness Members

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            return  new Dictionary<int, string>();
        }

        #endregion
    }
}
