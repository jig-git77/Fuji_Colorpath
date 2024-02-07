#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class CPSyncPrintStandardTypeData : CPSyncPrintStandardType
    {
        #region uspGetAllSyncPrintStandardType
        /// <summary>
        /// Get All Sync Print Standard Type
        /// </summary>
        /// <returns>returns the All Print Standard Type data</returns>
        public DataTable uspGetAllSyncPrintStandardType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetAllSyncPrintStandardType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion


        #region uspGetCertifiedProjectDropDown
        /// <summary>
        /// Get Certified Project Dropdown
        /// </summary>
        /// <returns>returns the All Print Standard Type data</returns>
        public DataTable uspGetCertifiedProjectForPress(int pressid)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@PressID", pressid);            
            Cmd.CommandText = "CPSync.uspGetCertifiedProjectForPress";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

    }
}
