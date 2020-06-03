#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion


namespace Fuji.Business
{
    public class CPSyncPrintStandardData : CPSyncPrintStandard
    {
        #region uspGetPrintStandard
        /// <summary>
        /// Get All Print Standard By Print Standard Type ID
        /// </summary>
        /// <param name="_PrintStandardTypeID">PrintStandardTypeID</param>
        /// <returns></returns>

        public DataTable uspGetPrintStandard(int _PrintStandardTypeID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPrintStandard";
            Cmd.Parameters.AddWithValue("@PrintStandardTypeID", _PrintStandardTypeID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion


    }
}
