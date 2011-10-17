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
    public class CPDongleData : CPDongle
    {
        #region Insert
        /// <summary>
        /// Inserts new Dongle
        /// </summary>
        public void Insert()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertDongle";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@CPSerialNUM", this.CPSerialNUM);
            Cmd.Parameters.AddWithValue("@DongleDescription", this.DongleDescription);
            Cmd.Parameters.AddWithValue("@AllowedIPFrom", this.AllowedIPFrom);
            Cmd.Parameters.AddWithValue("@AllowedIPTo", this.AllowedIPTo);
            Cmd.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);
            Cmd.Parameters.AddWithValue("@LastModifiedDate", this.LastModifiedDate);
            Cmd.Parameters.AddWithValue("@IsUsed", this.IsUsed);
            Cmd.Parameters.AddWithValue("@DongleTypeID",this.DongleTypeID) ;
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a dongle
        /// </summary>
        public void Update()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspUpdateDongle";
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@CPSerialNUM", this.CPSerialNUM);
            Cmd.Parameters.AddWithValue("@DongleDescription", this.DongleDescription);
            Cmd.Parameters.AddWithValue("@AllowedIPFrom", this.AllowedIPFrom);
            Cmd.Parameters.AddWithValue("@AllowedIPTo", this.AllowedIPTo);
            Cmd.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);
            Cmd.Parameters.AddWithValue("@LastModifiedDate", this.LastModifiedDate);
            Cmd.Parameters.AddWithValue("@IsUsed", this.IsUsed);
            Cmd.Parameters.AddWithValue("@DongleTypeID", this.DongleTypeID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deltes a dongle
        /// </summary>
        public void spDelete()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspDeleteDongle";
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region Retrieve Dongle Data by BranchID
        /// <summary>
        /// Retrieve a dongle using BranchID
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList uspRetrieveDongleByBranchID()
        {
            //========== Execute Command with SqlDataReader ==========//
            SqlCommand Cmd = new SqlCommand();
            System.Collections.ArrayList arrResult = new System.Collections.ArrayList();
            
            Cmd.CommandText = "CPV.uspGetDongleByBranchID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.DongleDescription);

            IDataReader IDR = (new Fuji.DataAccess.Database()).ExecuteDataReader(Cmd);// TPSDataAccess.TPSDADatabase.ExecuteDataReader(cmd, TPSDataAccess.TPSDBCSTRING.GetCSTRING(TPSDataAccess.TPSDBCSTRING.DBASE.TPSS))
            if (IDR == null)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                {
                    Cmd.Connection.Close();
                }
                return arrResult;
            }
            CPDongleData ObjDG;
            while (IDR.Read())
            {
                ObjDG = new CPDongleData();
                ObjDG.CPDongleID = Convert.ToInt32(IDR[0]);
                ObjDG.DongleDescription = IDR[1].ToString();
                arrResult.Add(ObjDG);
            }
            if (Cmd.Connection.State == ConnectionState.Open)
            {
                Cmd.Connection.Close();
            }
            return arrResult;
        }
        #endregion

        #region uspGetAllDongle
        /// <summary>
        /// Gets all Dongle
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllDongle()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDongle";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCheckDuplicateDongleNumber
        /// <summary>
        /// Check Duplicate Dongle
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetCheckDuplicateDongleNumber()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCheckDuplicateDongleNumber";
            Cmd.Parameters.AddWithValue("@CPDongleID", this.CPDongleID);
            Cmd.Parameters.AddWithValue("@CPSerialNUM", this.CPSerialNUM);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Get Dongle Data By Branch

        /// <summary>
        /// Used to get the Dongle List associated with the Branch
        /// </summary>
        /// <returns>returns the Table list of the Branches</returns>
        public DataTable uspGetDongleListByBranchID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDongleListByBranchID";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }


        #endregion

        #region Get Dongle Data By User ID
        /// <summary>
        /// Used to get the Dongle List associated with the Branch
        /// </summary>
        /// <param name="_UserID">User id of the logged in user</param>
        /// <param name="_RoleID">Role id of the logged in user</param>
        /// <param name="_CompanyID">Company id of the selected company</param>
        /// <returns>returns the Table list of the Branches</returns>
        public DataTable uspGetAllDongleByUserID(int _UserID, int _RoleID, int _CompanyID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDongleByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Get Dongle Data By Company ID
        /// <summary>
        /// Used to get the Dongle List associated with the Company
        /// </summary>
        /// <param name="_CompanyID">Company id of the selected company</param>
        /// <returns>returns the Table list of the Dongles</returns>
        public DataTable uspGetAllDongleByCompany(int _CompanyID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDongleByCompany";
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Get Dongle Data By Branch ID
        /// <summary>
        /// Used to get the Dongle List associated with the Branch
        /// </summary>
        /// <param name="_BranchID">Company id of the selected branch</param>
        /// <returns>returns the Table list of the Dongles</returns>
        public DataTable uspGetAllDongleByBranch(int _BranchID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllDongleByBranch";
            Cmd.Parameters.AddWithValue("@BranchID", _BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
