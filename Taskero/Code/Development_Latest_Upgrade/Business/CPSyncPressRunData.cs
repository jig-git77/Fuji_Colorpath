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
    public class CPSyncPressRunData : CPSyncPressRun
    {
        #region uspInsertPressRun
        /// <summary>
        /// Inserts new CPSync Press Run
        /// </summary>
        public void uspInsertCPSyncPressRun()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertPressRun";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID", this.ProjectID);
            Cmd.Parameters.AddWithValue("@Status", this.Status);
            Cmd.Parameters.AddWithValue("@StartDatetime", this.StartDateTime);
            SqlParameter param = new SqlParameter("@PressRunID", -1);
            param.Direction = ParameterDirection.Output;
            Cmd.Parameters.Add(param);
            param = new SqlParameter("@PressRunName", string.Empty);
            param.Direction = ParameterDirection.Output;
            Cmd.Parameters.Add(param);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            this.PressRunID = Convert.ToInt16(Cmd.Parameters["@PressRunID"].Value);
            this.PressRunName = Cmd.Parameters["@PressRunName"].Value.ToString();
        }
        #endregion

        #region uspInsertProject
        /// <summary>
        /// Inserts new CPSync Project
        /// </summary>
        public KeyValuePair<int, int> uspInsertCPSyncProject()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertProject";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectName", this.ProjectName);
            Cmd.Parameters.AddWithValue("@PressID", this.PressID);
            Cmd.Parameters.AddWithValue("@ΔEType", this.ΔEType);
            Cmd.Parameters.AddWithValue("@StartDatetime", this.StartDateTime);
            Cmd.Parameters.AddWithValue("@UseDLP", this.UseDLP);
            Cmd.Parameters.AddWithValue("@ProjectTypeID", this.ProjectTypeID);
            Cmd.Parameters.AddWithValue("@OutputTypeID", this.OutputTypeID);
            if (this.ReTargetID != 0 ) Cmd.Parameters.AddWithValue("@ReTargetID", this.ReTargetID);     
            SqlParameter param = new SqlParameter("@ProjectID", -1);
            param.Direction = ParameterDirection.Output;
            Cmd.Parameters.Add(param);
            param = new SqlParameter("@PressRunID", -1);
            param.Direction = ParameterDirection.Output;
            Cmd.Parameters.Add(param);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            return new KeyValuePair<int, int>(
                Convert.ToInt32(Cmd.Parameters["@ProjectID"].Value),
                Convert.ToInt32(Cmd.Parameters["@PressRunID"].Value));
        }
        #endregion

        #region uspGetAllSyncProjectByPressID
        /// <summary>
        /// Get All Projects By Press ID
        /// </summary>
        /// <param name="_PressID">Press ID</param>
        /// <returns></returns>
        public DataTable uspGetAllSyncProjectByPressID(int _PressID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetAllSyncProjectByPressID";
            Cmd.Parameters.AddWithValue("@PressID", _PressID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllSyncPressRunByProjectID
        /// <summary>
        /// Get All Press Run By Press ID
        /// </summary>
        /// <param name="_PressID">Press ID</param>
        /// <returns></returns>
        public DataTable uspGetAllSyncPressRunByProjectID(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetAllSyncPressRunByProjectID";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion

        #region uspGetSyncPressRunDetailByPressRunID
        /// <summary>
        /// Get All Press Run By Press Run ID
        /// </summary>
        /// <param name="_PressAlignmentID">Press Run ID</param>
        /// <returns></returns>

        public DataTable uspGetSyncPressRunDetailByPressRunID(int _PressRunID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncPressRunDetailByPressRunID";
            Cmd.Parameters.AddWithValue("@PressRunID", _PressRunID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion

        #region uspGetSyncPressRunDetailByProjectID
        /// <summary>
        /// Get Project Detail By Project ID
        /// </summary>
        /// <param name="_ProjectID">Project ID</param>
        /// <returns></returns>
        public DataTable uspGetSyncPressRunDetailByProjectID(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncPressRunDetailByProjectID";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        #endregion


        #region uspUpdateProjectStatus
        /// <summary>
        /// Update Project To UnUsed
        /// </summary>
        /// <param name="_ProjectID">Project ID</param>
        /// <returns></returns>
        public void uspUpdateProjectStatus()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspUpdateProjectStatus";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@ProjectID", this.ProjectID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);

        }
        #endregion

        #region uspGetICCProfile
        /// <summary>
        /// Get ICC Profile
        /// </summary>
        /// <returns></returns>
        public SqlDataReader uspGetICCProfile()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetICCProfile";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            return (new DataAccess.Database()).GetDataReader(Cmd);
        }
        #endregion

        #region uspUpdatePressRunStatus
        /// <summary>
        /// Update status = 2 for close press run.
        /// </summary>
        /// <returns></returns>
        public void uspUpdatePressRunStatus()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspUpdatePressRunStatus";
            Cmd.Parameters.AddWithValue("@PressRunID", this.PressRunID);
            Cmd.Parameters.AddWithValue("@Status", this.Status);
            Cmd.Parameters.AddWithValue("@EndDateTime", this.EndDateTime);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetPressRunStatus
        /// <summary>
        /// Get Status of passed press run id
        /// </summary>
        /// <param name="_PressID">Press ID</param>
        /// <returns></returns>
        public DataTable uspGetPressRunStatus(int _PressRunID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPressRunStatus1";
            Cmd.Parameters.AddWithValue("@PressRunID", _PressRunID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }

        /// <summary>
        /// Get Status of passed project id
        /// </summary>
        /// <param name="_ProjectID">Project ID</param>
        /// <returns></returns>
        public DataTable uspGetProjectStatus(int _ProjectID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetProjectStatus";
            Cmd.Parameters.AddWithValue("@ProjectID", _ProjectID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region UpdateProject
        /// <summary>
        /// Update Project
        /// </summary>
        /// <returns></returns>
        public void uspUpdateProject()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspUpdateProject";
            Cmd.Parameters.AddWithValue("@ProjectID", this.ProjectID);
            Cmd.Parameters.AddWithValue("@UseDLP", this.UseDLP);
            Cmd.Parameters.AddWithValue("@ProjectTypeID", this.ProjectTypeID);
            Cmd.Parameters.AddWithValue("@OutputTypeID", this.OutputTypeID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        /// <summary>
        /// check and make sure that project with similar name does not exists for that press
        /// </summary>
        /// <param name="ProjectName">Project Name</param>
        /// <param name="PressID">PressID</param>    
        /// <returns></returns>
        public SqlDataReader CheckProjectName()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "SELECT * FROM CPSync.Project WHERE PressID = @PressID AND ProjectName = @ProjectName";
            Cmd.Parameters.AddWithValue("@ProjectName", this.ProjectName);
            Cmd.Parameters.AddWithValue("@PressID", this.PressID);
            return (new DataAccess.Database()).GetDataReader(Cmd);
        }
        
    }
}
