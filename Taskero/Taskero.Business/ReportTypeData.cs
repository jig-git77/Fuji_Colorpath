#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class ReportTypeData : ReportType
    {
        #region uspGetAllReportType
        /// <summary>
        /// Get All the Report Types
        /// </summary>
        /// <returns>returns the report type data</returns>
        public DataTable uspGetAllReportType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllReportType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
