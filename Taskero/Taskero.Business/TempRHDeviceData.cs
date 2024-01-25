#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace Taskero.Business
{
    public class TempRHDeviceData : TempRHDevice
    {
        #region uspGetTempRHDeviceData
        /// <summary>
        /// Gets the TempRHDevice Data
        /// </summary>
        /// <returns>returns the TempRHDevice Data</returns>
        public DataSet uspGetTempRHDeviceData()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetTempRHDeviceData";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TempRHDeviceID", this.TempRHDeviceID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion   

        #region uspGetTempRHDeviceData1
        /// <summary>
        /// Gets the TempRHDevice Data
        /// </summary>
        /// <returns>returns the TempRHDevice Data</returns>
        public DataTable uspGetTempRHDeviceData1( string _BranchIDs )
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetTempRHDeviceData1";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchIDs", _BranchIDs);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion  

        #region Inserting the TempRHDevice
        /// <summary>
        /// Inserting the TempRHDevice.
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspInsertTempRHDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertTempRHDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@TRHDeviceSerialNum", this.TRHDeviceSerialNum);
            Cmd.Parameters.AddWithValue("@TRHDeviceTypeID", this.TRHDeviceTypeID);            
            Cmd.Parameters.AddWithValue("@TargetTemp", this.TargetTemp);
            Cmd.Parameters.AddWithValue("@TargetRH", this.TargetRH);
            Cmd.Parameters.AddWithValue("@Device", this.Device);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }        
        
        #endregion

        #region Updating the TempRHDevice
        /// <summary>
        /// Updating the TempRHDevice
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspUpdateTempRHDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateTempRHDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TempRHDeviceID", this.TempRHDeviceID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@TRHDeviceSerialNum", this.TRHDeviceSerialNum);
            Cmd.Parameters.AddWithValue("@TRHDeviceTypeID", this.TRHDeviceTypeID);            
            Cmd.Parameters.AddWithValue("@TargetTemp", this.TargetTemp);
            Cmd.Parameters.AddWithValue("@TargetRH", this.TargetRH);
            Cmd.Parameters.AddWithValue("@Device", this.Device);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }                
        #endregion        

        #region Deleting the TempRHDevice Row
        /// <summary>
        /// Deleting the TempRHDevice Row
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspDeleteTempRHDevice()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspDeleteTempRHDevice";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TempRHDeviceID", this.TempRHDeviceID);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion        
    }
}
