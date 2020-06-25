#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class PressJobsData : PressJobs
    {
        #region usp Get  Press Job Information
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobInformation()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobInformation";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region usp Get Press Job Summary
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobSummary()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobDeltaESummary";
                //Cmd.CommandText = "Press.uspGetJobDeltaESummary_OptiMized_Manish";

                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region usp Get Press Job Sheet Summary
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobSheetSummary()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobSheetDeltaESummary";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


        #region usp Get Press Job Run Admin Sheet Summary
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobRunAdminSheetSummary()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobRunAdminSheetDeltaESummary";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region usp Get Press Job Sheet Summary For a particular Sheet
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetDeltaESummaryForSheet(int _SheetNumber)
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetSheetDeltaESummary";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region usp Get Press Job Zone Summary
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobZoneSummary()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobZoneDeltaESummary";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion


        #region usp Get Press JobRunAdmin Zone Summary
        /// <summary>
        /// Retrieves Job info using job id.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetPressJobRunAdminZoneSummary()
        {
            try
            {
                //========== Execute Command with SqlDataReader ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Press.uspGetJobRunAdminZoneDeltaESummary";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                return (new DataAccess.Database()).GetDataTable(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Updateing the Press Job Run Admin IsUsed
        /// <summary>
        /// Updateing the Press Job Run Admin IsUsed
        /// </summary>
        /// <param name="_IsUsed">pass True Or False</param>        
        /// <param name="_ZoneNumber">Zone Number</param>        
        /// <returns>returns the int value as a result</returns>
        public void uspUpdatePressJobRunAdminIsUsed(Int16 _ZoneNumber)
        {
            try
            {
                //========== Execute Command ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "[Press].[uspUpdateJobZonesIsUsed]";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                Cmd.Parameters.AddWithValue("@ZoneNumber", _ZoneNumber);
                (new Database()).ExecuteNonQuery(Cmd);           

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Updateing the Press Sheets Run Admin IsUsed
        /// <summary>
        /// Updateing the Press Job Run Admin IsUsed
        /// </summary>
        /// <param name="_IsUsed">pass True Or False</param>        
        /// <param name="_ZoneNumber">Zone Number</param>        
        /// <returns>returns the int value as a result</returns>
        public void uspUpdatePressSheetesRunAdminIsUsed(Int16 _SheetNumber)
        {
            try
            {
                //========== Execute Command ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "[Press].[uspUpdateSheetsIsUsed]";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@JobID", this.JobID);
                Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
                Cmd.Parameters.AddWithValue("@SheetNumber", _SheetNumber);
                (new Database()).ExecuteNonQuery(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
