#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class ReportPressMakeReadyData : ReportPressMakeReady
    {

        #region [Press].[GetComapnyFromUserID]
        public DataSet GetComapnyFromUserID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Press].[GetComapnyFromUserID]";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region [Press].[GetBranchFromUserID]
        public DataTable GetBranchFromUserID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Press].[GetBranchFromUserID]";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd) ;
        }
        #endregion

        #region [Press].[GetPressDevicesFromBranch]
        public DataTable GetPressDevicesFromBranch()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Press].[GetPressDevicesFromBranch]";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region [Report].[usp_MakeReadySearchResult]
        public int usp_MakeReadySearchResult()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Report].[usp_MakeReadySearchResult]";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@CPDeviceID", this.CPDeviceID);
            if(JobName.Trim()!="")
                Cmd.Parameters.AddWithValue("@JobName", this.JobName);

            Cmd.Parameters.AddWithValue("@StartDT", this.StartDate);
            Cmd.Parameters.AddWithValue("@EndDT", this.EndDate);
            Cmd.Parameters.AddWithValue("@StartTime", this.StartTime);
            Cmd.Parameters.AddWithValue("@EndTime", this.EndTime);

            Cmd.Parameters.Add(new SqlParameter("@MakeReadyReportID", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));


            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            if(Cmd.Parameters["@MakeReadyReportID"].Value.ToString().Trim()!="")
                return Convert.ToInt32(Cmd.Parameters["@MakeReadyReportID"].Value.ToString());

            return 0;
        }
        #endregion

        #region [Report].[usp_MakeReadyGetBranchIDs]
        public DataTable usp_MakeReadyGetBranchIDs()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            //Cmd.CommandText = "SELECT DISTINCT BranchID FROM  Report.StagingMakeReadyData  WHERE MakeReadyReportID = @MakeReadyDataID";
            Cmd.CommandText = "SELECT DISTINCT Report.StagingMakeReadyData.BranchID,  Press.vwCompanyWithPress.BranchName FROM Press.vwCompanyWithPress INNER JOIN Report.StagingMakeReadyData ON Press.vwCompanyWithPress.BranchID = Report.StagingMakeReadyData.BranchID Where Report.StagingMakeReadyData.MakeReadyReportID = @MakeReadyDataID";
            Cmd.Parameters.AddWithValue("@MakeReadyDataID", this.MakeReadyReportID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region [Report].[usp_MakeReadyBranchSummary]
        public DataSet usp_MakeReadyBranchSummary()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Report].[usp_MakeReadyBranchSummary]";
            Cmd.Parameters.AddWithValue("@MakeReadyReportID", this.MakeReadyReportID);
            Cmd.Parameters.AddWithValue("@BranchID", int.Parse(this.BranchID));
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region [Report].[usp_MakeReadyCompanySummary]
        public DataSet usp_MakeReadyCompanySummary()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "[Report].[usp_MakeReadyCompanySummary]";
            Cmd.Parameters.AddWithValue("@MakeReadyReportID", this.MakeReadyReportID);            
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion


    }
}
