using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ReferenceChartBusinessMock : IReferenceChartBusiness
    {
        #region IReferenceChartTypeBusiness Members

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            return  new Dictionary<int, string>();
        }

        public ReferenceChartData GetReferenceChartData(int projectId)
        {
            return new ReferenceChartData();
        }

        #endregion
    }
}
