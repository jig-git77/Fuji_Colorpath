#region "Using System"
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region "Using Applications"
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class CPDeviceData : CPDevice
    {
        #region uspInsertDevice
        /// <summary>
        /// Inserts new Device
        /// </summary>
        public void uspInsertDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@CPDeviceDesc", this.CPDeviceDesc);
            Cmd.Parameters.AddWithValue("@MeasurementTypeID", this.MeasurementTypeID);
            Cmd.Parameters.AddWithValue("@CPDeviceTypeID", this.CPDeviceTypeID);
            Cmd.Parameters.AddWithValue("@LastMeasurementID", this.LastMeasurementID);            
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspUpdateDevice
        /// <summary>
        /// Updates a Device
        /// </summary>
        public void uspUpdateDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspUpdateDevice";
            Cmd.Parameters.AddWithValue("@CPDeviceID", this.CPDeviceID);
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@CPDeviceDesc", this.CPDeviceDesc);
            Cmd.Parameters.AddWithValue("@MeasurementTypeID", this.MeasurementTypeID);
            Cmd.Parameters.AddWithValue("@CPDeviceTypeID", this.CPDeviceTypeID);
            Cmd.Parameters.AddWithValue("@LastMeasurementID", this.LastMeasurementID);      
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspDeleteDevice
        /// <summary>
        /// Deltes a Device
        /// </summary>
        public void uspDeleteDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspDeleteDevice";
            Cmd.Parameters.AddWithValue("@CPDeviceID", this.CPDeviceID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspSelectDistinctDevices
        /// <summary>
        /// Gets all Device
        /// </summary>
        /// <param name="_UserID">User id of the logged in user</param>
        /// <param name="_RoldID">Role id of the logged in user</param>
        /// <returns>returns the All Device Data </returns>
        public DataTable uspSelectDistinctDevices(int _UserID,int _RoldID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspSelectDistinctDevices";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoldID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllDeviceByUserID
        /// <summary>
        /// Used to get all device list by user id and its role
        /// </summary>
        /// <param name="_UserID">UserID of the logged in user</param>
        /// <param name="_RoleID">RoleID of the logged in user</param>
        /// <returns>returns the device data table list</returns>
        public DataTable uspGetAllDeviceByUserID(int _UserID , int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDeviceByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllDevice
        /// <summary>
        /// Gets all Device
        /// </summary>
        /// <returns>returns the All Device Data </returns>
        public DataTable uspGetAllDevice()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDevice";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviceByDongleID
        /// <summary>
        /// Gets The Device List BY Dongle ID
        /// </summary>
        /// <returns>returns the All Device of the particular Dongle Number</returns>
        public DataTable uspGetDeviceByDongleID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviceByDongleID";
            Cmd.Parameters.AddWithValue("@DongleID", this.CPDongleID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDeviceByDongleIDColors
        /// <summary>
        /// Gets The Device List BY Dongle ID
        /// </summary>
        /// <returns>returns the All Device of the particular Dongle Number and Colors(MeasurementTypeID = 1)</returns>
        public DataTable uspGetDeviceByDongleIDColors()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDeviceByDongleIDColors";
            Cmd.Parameters.AddWithValue("@DongleID", this.CPDongleID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetProofAndPlateAdmin
        /// <summary>
        /// Gets the Proof and Plate Records for Admin.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetProofAndPlateAdmin()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetProofAndPlateAdmin";            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetProofAndPlateByTaskeroUserID
        /// <summary>
        /// Gets the Proof and Plate Records for Users.
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetProofAndPlateByTaskeroUserID(int _TaskeroUserID)
        {            
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetProofAndPlateByTaskeroUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
