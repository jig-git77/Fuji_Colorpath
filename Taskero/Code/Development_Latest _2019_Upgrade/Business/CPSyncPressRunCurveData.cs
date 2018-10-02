#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion


namespace Fuji.Business
{
    public class CPSyncPressRunCurveData : CPSyncPressRunCurve
    {
        #region uspGetAllSyncPressRunByPressID
        /// <summary>
        /// Get All Press Run By Press ID
        /// </summary>
        /// <param name="_PressID">Press ID</param>
        /// <returns></returns>

        public DataSet uspGetCurveData(int _CurveTypeID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetCurveData";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            Cmd.Parameters.AddWithValue("@CurveTypeID", _CurveTypeID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }

        #endregion
    }
}
