#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class CPDevicePrintTargetData : CPDevicePrintTarget
    {
        #region uspGetPrintTargetByDeviceID
        /// <summary>
        /// Gets The Device List BY Dongle ID
        /// </summary>
        /// <returns>returns the All Device of the particular Dongle Number</returns>
        public DataTable uspGetPrintTargetByDeviceID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetPrintTargetByDeviceID";
            Cmd.Parameters.AddWithValue("@DeviceID", this.DeviceID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
