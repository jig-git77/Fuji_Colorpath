#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPDeviceTypeData : CPDeviceType
    {
        #region uspGetAllDeviceType
        /// <summary>
        /// Get All DeviceType
        /// </summary>
        /// <returns>returns the All Device Type data</returns>
        public DataTable uspGetAllDeviceType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "CPV.uspGetAllDeviceType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDistinctDeviceTypeByUserID
        /// <summary>
        /// Used to get all DeviceType by user id and its role
        /// </summary>
        /// <param name="_UserID">ID of the user</param>
        /// <param name="_RoleID">RoleID of the current user</param>
        /// <returns>returns the DeviceType Data table list</returns>
        public DataTable uspGetDistinctDeviceTypeByUserID(int _UserID, int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDistinctDeviceTypeByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
