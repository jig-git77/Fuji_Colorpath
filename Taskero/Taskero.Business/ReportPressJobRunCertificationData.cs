#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Taskero.Business
{
    public class ReportPressJobRunCertificationData : ReportPressJobRunCertification
    {
        #region usp_Press_GetColorCharsGray_P
        public DataSet GetColorCharsGray_P()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetColorCharsGray_P";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobDeltaESummary_NP
        public DataSet GetJobDeltaESummary_NP()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobDeltaESummary_NP";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobDeltaESummary_P
        public DataSet GetJobDeltaESummary_P()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobDeltaESummary_P";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobInformation_NP
        public DataSet GetJobInformation_NP()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobInformation_NP";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobInformation_P
        public DataSet GetJobInformation_P()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobInformation_P";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobMetrics_NP
        public DataSet GetJobMetrics_NP()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobMetrics_NP";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobMetrics_P
        public DataSet GetJobMetrics_P()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobMetrics_P";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobReportGrid
        public DataSet GetJobReportGrid()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobReportGrid";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region usp_Press_GetJobTrendView
        public DataSet GetJobTrendView()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "Report.usp_Press_GetJobTrendView";
            Cmd.Parameters.AddWithValue("@JobID", this.JobID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

    }
}
