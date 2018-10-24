#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion


namespace Fuji.Business
{
    public class ReportData : Report
    {
        #region uspGetReportByReportTypeID
        /// <summary>
        /// Get All the Report Types
        /// </summary>
        /// <returns>returns reports data</returns>
        public DataTable uspGetReportByReportTypeID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetReportByReportTypeID";
            Cmd.Parameters.AddWithValue("@ReportTypeID", this.ReportTypeID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllReports
        /// <summary>
        /// Get All the Report Types.
        /// </summary>
        public DataTable uspGetAllReports(int _TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllReports";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllReportsOnTaskeroUserID
        /// <summary>
        /// Get All the Report Types by UserID
        /// </summary>
        public DataTable uspGetAllReportsOnTaskeroUserID(int _TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllReportsOnTaskeroUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
