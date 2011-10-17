using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Fuji.Business
{
    public class CPVMobiJobDeviceData : CPVMobiJobDevice
    {
        #region uspGetMobiJobMetrics
        public DataTable uspGetMobiJobMetrics()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetMobiJobMetrics";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetMobiDeviceTrend
        public DataTable uspGetMobiDeviceTrend()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetMobiDeviceTrend";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);
            Cmd.Parameters.AddWithValue("@PrintTarget", this.PrintTarget);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
