#region System
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Taskero.Business
{
    public class CPPatchDataPlateData : CPPatchDataPlate
    {
        
        #region uspGetDeviationByCPDeviceID_Admin
        /// <summary>
        /// Used to get datiations data
        /// </summary>
        /// <returns>returns the deviation data by device id</returns>
        public DataTable uspGetDeviationByCPDeviceID_Admin(int _CPDeviceID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviationByCPDeviceID_Admin";
            Cmd.Parameters.AddWithValue("@CPDeviceID", _CPDeviceID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviationByCPDeviceID
        /// <summary>
        /// Used to get datiations data
        /// </summary>
        /// <returns>returns the deviation data by device id</returns>
        public DataTable uspGetDeviationByCPDeviceID(int _CPDeviceID , int _TaskeroUserID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviationByCPDeviceID";
            Cmd.Parameters.AddWithValue("@CPDeviceID", _CPDeviceID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviationByCPDevicePrintTargetID_Admin
        /// <summary>
        /// Used to get datiations data
        /// </summary>
        /// <returns>returns the deviation data by cp device PRINT TARGET id</returns>
        public DataTable uspGetDeviationByCPDevicePrintTargetID_Admin(int _CPDevicePrintTargetID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviationByCPDevicePrintTargetID_Admin";
            Cmd.Parameters.AddWithValue("@CPDevicePrintTargetID", _CPDevicePrintTargetID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviationByCPDevicePrintTargetID
        /// <summary>
        /// Used to get datiations data
        /// </summary>
        /// <returns>returns the deviation data by cp device PRINT TARGET id</returns>
        public DataTable uspGetDeviationByCPDevicePrintTargetID(int _CPDevicePrintTargetID , int _TaskeroUserID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviationByCPDevicePrintTargetID";
            Cmd.Parameters.AddWithValue("@CPDevicePrintTargetID", _CPDevicePrintTargetID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviationByGUID
        /// <summary>
        /// Used to get datiations data
        /// </summary>
        /// <returns>returns the deviation data by guid</returns>
        public DataTable uspGetDeviationByGUID()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviationByGUID";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAveMaxMinDeviation
        /// <summary>
        /// Extracts the average, minimum and the maximum deviation.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAveMaxMinDeviation()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAveMaxMinDeviation";
            Cmd.Parameters.AddWithValue("@MeasurementGUID", this.MeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetMeasurementFile
        /// <summary>
        /// Download MeasurementFile 
        /// </summary>
        public DataTable uspGetMeasurementFilePlate()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetMeasurementFile";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.MeasurementGUID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
