using System.Collections.Generic;
using System.Linq;
using System.Data;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ReferenceChartTypeBusinessImpl : IReferenceChartTypeBusiness
    {
        #region IReferenceChartTypeBusiness Members

        public Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId(int measurementDeviceId)
        {
            var dstReferenceChartTypeList = new Dictionary<int, string>();
            //========== Get data from data access=================//
            var dsReferenceChartTypeList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetReferenceChartTypeByMeasurementDeviceID]", new object[] { measurementDeviceId });


            if (dsReferenceChartTypeList != null && dsReferenceChartTypeList.Tables.Count > 0)
            {
                dstReferenceChartTypeList = (dsReferenceChartTypeList.Tables[0].AsEnumerable().
                    Select(referenceChartType => new
                                                     {
                                                         Value = DsReader.ReadInt(referenceChartType, "ReferenceChartTypeID"),
                                                         Text = DsReader.ReadString(referenceChartType, "ChartName")
                                                     })).ToDictionary(referenceChartType => referenceChartType.Value, referenceChartType => referenceChartType.Text);

            }
            return dstReferenceChartTypeList;
        }

        #endregion
    }
}
