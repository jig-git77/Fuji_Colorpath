#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace Oris.Business
{
    public class TempRHDataData : TempRHData
    {
        #region uspGetTempRHData
        /// <summary>
        /// Gets the Information of last temperature recorded using CPeviceID.
        /// </summary>
        /// <returns>returns the top1 temp rh data</returns>
        public DataTable uspGetTop1TempRHData(int _CPeviceID)
        { 
            //========== Sql Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetTop1TempRHData";
            Cmd.Parameters.AddWithValue("@CPDeviceID", _CPeviceID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
