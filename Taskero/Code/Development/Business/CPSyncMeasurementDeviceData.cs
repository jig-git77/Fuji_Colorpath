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
    public class CPSyncMeasurementDeviceData : CPSyncMeasurementDevice
    {
        #region Insert
        /// <summary>
        /// Inserts new Sync Measurement Device
        /// </summary>
        public void Insert()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertSyncMeasurementDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@SerialNumber", this.SerialNumber);
            Cmd.Parameters.AddWithValue("@DeviceTypeID", this.DeviceTypeID);            
            Cmd.Parameters.AddWithValue("@IsActive", this.isActive);

            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a Sync Measurement Device
        /// </summary>
        public void Update()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspUpdateSyncMeasurementDevice";
            Cmd.Parameters.AddWithValue("@CPSyncMeasurementDeviceID", this.CPSyncMeasurementDeviceID);
            Cmd.Parameters.AddWithValue("@SerialNumber", this.SerialNumber);
            Cmd.Parameters.AddWithValue("@DeviceTypeID", this.DeviceTypeID);
            Cmd.Parameters.AddWithValue("@IsActive", this.isActive);

            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion      

        #region Delete        
        public void Delete()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspDeleteSyncMeasurementDevice";
            Cmd.Parameters.AddWithValue("@CPSyncMeasurementDeviceID", this.CPSyncMeasurementDeviceID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        
        #endregion

        #region GetSyncMeasurementDevice
        /// <summary>
        /// Get Sync Measurement Device used by Sync Measurement Device
        /// </summary>
        public DataTable uspGetSyncMeasurementDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncMeasurementDevice";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion  

        #region GetSyncMeasurementDeviceType
        /// <summary>
        /// Get Sync Measurement Device Type used by Sync Measurement Device Type
        /// </summary>
        public DataTable uspGetSyncMeasurementDeviceType()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncMeasurementDeviceType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion  

        #region GetMeasurementDeviceTypeByPressID
        /// <summary>
        /// Get Measurement Device Type By PressID
        /// </summary>
        public DataTable uspGetMeasurementDeviceTypeByPressID()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspGetMeasurementDeviceTypeByPressID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@PressID", this.PressID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
