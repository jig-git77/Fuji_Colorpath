#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace Fuji.Business
{
    public class TempRHDevice_CPDeviceData : TempRHDevice_CPDevice
    {
        #region uspGetDTLTrendTempRHForDevice
        /// <summary>
        /// Gets the temperature on device.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetDTLTrendTempRHForDevice()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendTempRHForDevice";
            Cmd.Parameters.AddWithValue("@CPDeviceID" , this.CPDeviceID); 
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
