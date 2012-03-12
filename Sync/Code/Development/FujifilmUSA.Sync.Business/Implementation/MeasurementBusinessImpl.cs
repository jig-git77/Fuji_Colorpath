using System;
using System.Data;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.Messages;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementBusinessImpl : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public MeasurementDataResponse GetMeasurementData(int[] measurementIds)
        {
            var response = new MeasurementDataResponse();
            var dsPcsDcsData = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetMeasurementData", new object[] { String.Join(",", measurementIds) });
            if (dsPcsDcsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                {
                    response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                    response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                    response.PcsData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));
                    response.DcsData.Add(DataReader.ReadDouble(row, "CIELAB_L*"));
                    response.DcsData.Add(DataReader.ReadDouble(row, "CIELAB_A*"));
                    response.DcsData.Add(DataReader.ReadDouble(row, "CIELAB_B*"));
                }
            }
            return response;
        }

        #endregion
    }
}