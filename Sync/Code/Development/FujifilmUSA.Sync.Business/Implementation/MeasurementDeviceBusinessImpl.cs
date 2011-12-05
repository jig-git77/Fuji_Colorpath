using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementDeviceBusinessImpl : IMeasurementDeviceBusiness
    {
        #region IMeasurementDeviceBusiness Members

        public Dictionary<int, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {
            
            var dstMeasurementDeviceList = new Dictionary<int, string>();
            //========== Get data from data access=================//
            var dsMeasurementDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementDeviceBySyncDeviceID]", new object[] { syncDeviceId });


            if (dsMeasurementDeviceList != null && dsMeasurementDeviceList.Tables.Count > 0 )
            {
                dstMeasurementDeviceList = (dsMeasurementDeviceList.Tables[0].AsEnumerable().
                    Select(measurementDevice => new
                                                    {
                                                        Value = DataReader.ReadInt(measurementDevice, "MeasurementDeviceID"),
                                                        Text =
                                                    DataReader.ReadString(measurementDevice, "MeasurementDeviceTypeName") + DataReader.ReadString(measurementDevice, "SerialNumber")
                                                    })).ToDictionary(measurementDevice => measurementDevice.Value, measurementDevice => measurementDevice.Text);

            }
            return dstMeasurementDeviceList;
        }

        #endregion
    }
}
