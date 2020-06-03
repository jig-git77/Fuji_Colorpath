#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class CPMeasurementData : CPMeasurement
    {
        #region uspGetCPMeasurementGUIDByCPDeviceID
        /// <summary>
        /// This retrieves the CPMeasurementGuid with DeviceID.
        /// </summary>
        /// <returns>returns the Measurement Data by device id</returns>
        public DataTable uspGetCPMeasurementGUIDByCPDeviceID(int _TaskeroUserID, int _RoleID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCPMeasurementGUIDByCPDeviceID";
            Cmd.Parameters.AddWithValue("@CPDeviceID", this.CPDeviceID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCPMeasurementGUIDByCPDevicePrintTargetID
        /// <summary>
        /// This retrieves the CPMeasurementGuid with CPDevicePrintTargetID.
        /// </summary>        
        public DataTable uspGetCPMeasurementGUIDByCPDevicePrintTargetID(int _CPDevicePrintTargetID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCPMeasurementGUIDByCPDevicePrintTargetID";
            Cmd.Parameters.AddWithValue("@CPDevicePrintTargetID", _CPDevicePrintTargetID);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCPMeasurementGUIDByCPDeviceIDs
        /// <summary>
        /// This retrieves the CPMeasurementGuid with DeviceIDs in below given format.
        /// </summary>
        /// <returns>returns the Measurement Data by device ids</returns>
        public DataTable uspGetCPMeasurementGUIDByCPDeviceIDs(string _CPDeviceIDs)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCPMeasurementGUIDByCPDeviceIDs";
            Cmd.Parameters.AddWithValue("@CPDeviceIDs", _CPDeviceIDs);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetJobReportDataOnGUID
        /// <summary>
        /// This retrieves the CPMeasurementGuid with DeviceID.
        /// </summary>
        /// <returns>returns the JobReport Data by GUID</returns>
        public DataTable uspGetJobReportDataOnGUID(string CPMeasurementGUID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetJobReportDataOnGUID";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspUpdateIsUsedFlagOnGUID
        /// <summary>
        /// Updates the IsUsed Flag to no on MeasurementGUID.
        /// </summary>
        public void uspUpdateIsUsedFlagOnGUID()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspUpdateIsUsedFlagOnGUID";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetDTLTrendDetails
        /// <summary>
        /// Gets all data for Trend Chart Line Details Page
        /// </summary>
        /// <returns>returns the DTLTrendDetails Data</returns>
        public DataSet uspGetDTLTrendDetails(int CPDevicePrintTargetID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLTrendDetails";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            Cmd.Parameters.AddWithValue("@CPDeviceID", CPDeviceID);
            Cmd.Parameters.AddWithValue("@CPDevicePrintTargetID", CPDevicePrintTargetID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetCheckCPVStatus
        /// <summary>
        /// Checks the IsUsed values.
        /// </summary>
        /// <returns>returns the CPVStatus Data</returns>
        public DataTable uspGetCheckCPVStatus()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCheckCPVStatus";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCPMeasurementData
        /// <summary>
        /// This retrieves the CPMeasurementGUID with GUID.
        /// </summary>
        /// <returns>returns the Measurement Data</returns>
        public DataTable uspGetCPMeasurementData(string CPMeasurementGUID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCPMeasurementData";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDTLDetailsPlateByGUID
        /// <summary>
        /// Used to get DTLDetailsPlate data by GUID
        /// </summary>
        /// <returns>returns DTLDetailsPlate Data by GUID </returns>
        public DataTable uspGetDTLDetailsPlateByGUID()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDTLDetailsPlateByGUID";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
    
        #endregion

        #region uspGetTOP100CPMeasurements
        public DataTable uspGetTOP100CPMeasurements(int _TaskeroUserID, int _RoleID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetTOP100CPMeasurements";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspDeleteMeasurementByGUIDs
        /// <summary>
        /// Deletes a measurement from database on GUIDs.
        /// Comma Sepaerated XXX-XXX-XXX,YYY-YYY-YYY,ZZZ-ZZZ-ZZZ
        /// </summary>
        public void uspDeleteMeasurementByGUIDs()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspDeleteMeasurementByGUIDs";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", this.CPMeasurementGUID);            
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetJobReportData_Admin
        /// <summary>
        /// Gets the job report for Admin.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetJobReportData_Admin(int _CompanyID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetJobReportData_Admin";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetJobReportData
        /// <summary>
        /// Gets the job report for user.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetJobReportData(int _CompanyID, int _TaskeroUserID)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetJobReportData";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviceReportData_Admin
        /// <summary>
        /// Gets the device report for Admin.
        /// </summary>
        public DataTable uspGetDeviceReportData_Admin(int _CompanyID,Boolean _Press)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviceReportData_Admin";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            Cmd.Parameters.AddWithValue("@Press", _Press);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviceReportData
        /// <summary>
        /// Gets the device report for user.
        /// </summary>
        public DataTable uspGetDeviceReportData(int _CompanyID, int _TaskeroUserID, Boolean _Press)
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviceReportData";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            Cmd.Parameters.AddWithValue("@Press", _Press);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion


        #region uspGetMeasurementFile
        /// <summary>
        /// Download MeasurementFile 
        /// </summary>
        public DataTable uspGetMeasurementFile()
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetMeasurementFile";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);

            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetTempRHData
        /// <summary>
        /// Gets the Information of last temperature recorded using CPeviceID.
        /// </summary>
        /// <returns>returns the top1 temp rh data</returns>
        public DataTable uspGetTempRHData()
        {
            //========== Sql Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetTempRHData";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCPJobPreview
        /// <summary>
        /// Gets the job preview image
        /// </summary>
        /// <returns>returns the top1 temp rh data</returns>
        public DataTable uspGetCPJobPreview()
        {
            //========== Sql Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CTW.uspGetCPJobPreview";
            Cmd.Parameters.AddWithValue("@CPMeasurementGUID", CPMeasurementGUID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
