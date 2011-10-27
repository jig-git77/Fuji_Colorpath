#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPMeasurementTypeData : CPMeasurementType
    {
        #region uspGetAllMeasurementType
        /// <summary>
        /// Get All MeasurementType
        /// </summary>
        /// <returns>returns the All Measurement Type data</returns>
        public DataTable uspGetAllMeasurementType()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "CPV.uspGetAllMeasurementType";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDistinctMeasurementTypeByUserID
        /// <summary>
        /// Used to get all MeasurementType by user id and its role
        /// </summary>
        /// <param name="_UserID">ID of the user</param>
        /// <param name="_RoleID">RoleID of the current user</param>
        /// <returns>returns the MeasurementType Data table list</returns>
        public DataTable uspGetDistinctMeasurementTypeByUserID(int _UserID, int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDistinctMeasurementTypeByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
