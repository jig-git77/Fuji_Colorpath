#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace Fuji.Business
{
    public class CPDeviceStatusData : CPDeviceStatus
    {
        #region uspInsertCPDeviceStatusResetNew
        /// <summary>
        /// Updates the final status.
        /// </summary>
        /// <returns></returns>
        public void uspInsertCPDeviceStatusResetNew()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspInsertCPDeviceStatusResetNew";
            Cmd.Parameters.AddWithValue("@CPDeviceID", this.CPDeviceID);
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion
    }
}
