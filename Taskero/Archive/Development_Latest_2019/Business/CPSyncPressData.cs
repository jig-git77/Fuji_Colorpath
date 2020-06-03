#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class CPSyncPressData : CPSyncPress
    {
        #region uspInsertPress
        /// <summary>
        /// Inserts new CPSync Press
        /// </summary>
        public void uspInsertCPSyncPress()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPSync.uspInsertPress";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@PressName", this.PressName);
            Cmd.Parameters.AddWithValue("@PressManufacturer", this.PressManufacturer);
            Cmd.Parameters.AddWithValue("@NumOfInkUnits", this.NumOfInkUnits);
            Cmd.Parameters.AddWithValue("@PressTypeID", this.PressTypeID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetAllSyncPress
        /// <summary>
        /// Get All Sync Press
        /// </summary>
        /// <returns>returns the All Press Type data</returns>
        public DataTable uspGetAllSyncPress(int _UserID, int _RoleID,int _CompanyID,int _BranchID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetPressListByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            Cmd.Parameters.AddWithValue("@BranchID", _BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion


        #region uspGetAllSyncPress
        /// <summary>
        /// Get All Company Name And Branch Name list
        /// </summary>
        /// <returns>returns the All Press Type data</returns>
        public DataTable uspGetCompanyListAndBranchList(int _CompanyID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetCompanyList";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetSyncPressDetailByPressID
        /// <summary>
        /// Get Press Detail By Press ID
        /// </summary>
        /// <param name="_PressID">Press ID</param>
        /// <returns></returns>
      
        public DataTable uspGetSyncPressDetailByPressID(int _PressID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPSync.uspGetSyncPressDetailByPressID";
            Cmd.Parameters.AddWithValue("@PressID", _PressID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
       
        #endregion

        #region uspGetPressOnDongleName
        /// <summary>
        /// Get Press list on Dongle Name - used for service proto-type
        /// </summary>
        /// <returns>returns the All Press Type data</returns>
        public DataTable uspGetPressOnDongleName(string dongleName)
        {
            var cmd = new SqlCommand
                          {
                              CommandType = CommandType.StoredProcedure,
                              CommandText = "Service.uspGetPressOnDongleName"
                          };
            cmd.Parameters.AddWithValue("@DongleName", dongleName);
            return (new DataAccess.Database()).GetDataTable(cmd);
        }
        #endregion
    }
}
