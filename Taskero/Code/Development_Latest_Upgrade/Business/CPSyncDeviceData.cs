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
    public class CPSyncDeviceData: CPSyncDevice
    {

        #region Insert
        /// <summary>
        /// Inserts new Sync Device
        /// </summary>
        public void Insert()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertSyncDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@CPSyncDeviceName", this.CPSyncDeviceName);
            Cmd.Parameters.AddWithValue("@CPSyncDeviceSerialNum", this.CPSyncDeviceSerialNum);
            Cmd.Parameters.AddWithValue("@IsUsed", this.isUsed);
           
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a Sync Device
        /// </summary>
        public void Update()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspUpdateSyncDevice";
            Cmd.Parameters.AddWithValue("@CPSyncDeviceID", this.CPSyncDeviceID);
            Cmd.Parameters.AddWithValue("@CPSyncDeviceName", this.CPSyncDeviceName);
            Cmd.Parameters.AddWithValue("@CPSyncDeviceSerialNum", this.CPSyncDeviceSerialNum);
            Cmd.Parameters.AddWithValue("@IsUsed", this.isUsed);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deltes a Sync Device
        /// </summary>
        //public void spDelete()
        //{
        //    //========== Execute Command ==========//
        //    SqlCommand Cmd = new SqlCommand();
        //    Cmd.CommandType = CommandType.StoredProcedure;
        //    Cmd.CommandText = "CPSync.uspDeleteSyncDevice";
        //    Cmd.Parameters.AddWithValue("@SyncDeviceID", this.);
        //    (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        //}
        #endregion  

        #region GetSyncDevice
        /// <summary>
        /// Get Sync Device used by Sync Device Management
        /// </summary>
        public DataTable uspGetSyncDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncDevice";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion  

        #region Get Sync Dongle Data By Branch ID
        /// <summary>
        /// Used to get the Dongle List associated with the Branch (Sync only)
        /// </summary>
        /// <param name="_BranchID">Branch id of the selected branch</param>
        /// <returns>returns the Table list of the Dongles</returns>
        public DataTable uspGetDongleByBranch(int _BranchID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetDongleByBranch";
            Cmd.Parameters.AddWithValue("@BranchID", _BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

    }
}
