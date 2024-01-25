#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class CPSyncPrintTargetData : CPSyncPrintTarget
    {
        #region uspInsertPrintTarget
        /// <summary>
        /// Inserts new CPSync Print Target
        /// </summary>
        public void uspInsertCPSyncPrintTarget(int _ProjectID, StringBuilder _SB)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertPrintTarget";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            Cmd.Parameters.AddWithValue("@PrintTarget", _SB.ToString());
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion



        #region uspGetPrintTargetByProjectID
        /// <summary>
        /// Get Print Target By Project ID
        /// </summary>
        /// <param name="_PressRunID">ProjectID</param>
        /// <returns></returns>
        public DataTable uspGetPrintTargetByProjectID(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPrintTargetByProjectID";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion
    }
}
