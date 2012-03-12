using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementBusinessMock : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public MeasurementDataResponse GetMeasurementData(int[] measurementIds)
        {
            return new MeasurementDataResponse();
        }

        #endregion
    }
}
