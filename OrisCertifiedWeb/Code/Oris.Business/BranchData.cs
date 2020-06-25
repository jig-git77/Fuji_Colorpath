#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class BranchData : Branch
    {
        #region uspInsertBranch
        /// <summary>
        /// Used to insert New branch
        /// </summary>
        public int uspInsertBranch()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertBranch";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@BranchName", this.BranchName);
            Cmd.Parameters.AddWithValue("@BranchLocation", this.BranchLocation);
            Cmd.Parameters.AddWithValue("@PrimaryContactName", this.PrimaryContactName);
            Cmd.Parameters.AddWithValue("@Tel_CountryCode", this.Tel_CountryCode);
            Cmd.Parameters.AddWithValue("@Tel_PhoneNo", this.Tel_PhoneNo);
            Cmd.Parameters.AddWithValue("@Tel_Extension", this.Tel_Extension);
            Cmd.Parameters.AddWithValue("@PrimaryContactEmail", this.PrimaryContactEmail);
            Cmd.Parameters.AddWithValue("@PrimaryContactCell", this.PrimaryContactCell);
            Cmd.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);
            Cmd.Parameters.AddWithValue("@LastModifiedDate", this.LastModifiedDate);
            Cmd.Parameters.AddWithValue("@IsActive", this.IsActive);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspUpdateBranch
        /// <summary>
        /// Used to Update Existing Branch
        /// </summary>
        public int uspUpdateBranch()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspUpdateBranch";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@BranchName", this.BranchName);
            Cmd.Parameters.AddWithValue("@BranchLocation", this.BranchLocation);
            Cmd.Parameters.AddWithValue("@PrimaryContactName", this.PrimaryContactName);
            Cmd.Parameters.AddWithValue("@Tel_CountryCode", this.Tel_CountryCode);
            Cmd.Parameters.AddWithValue("@Tel_PhoneNo", this.Tel_PhoneNo);
            Cmd.Parameters.AddWithValue("@Tel_Extension", this.Tel_Extension);
            Cmd.Parameters.AddWithValue("@PrimaryContactEmail", this.PrimaryContactEmail);
            Cmd.Parameters.AddWithValue("@PrimaryContactCell", this.PrimaryContactCell);
            Cmd.Parameters.AddWithValue("@LastModifiedBy", this.LastModifiedBy);
            Cmd.Parameters.AddWithValue("@LastModifiedDate", this.LastModifiedDate);
            Cmd.Parameters.AddWithValue("@IsActive", this.IsActive);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspDeleteBranch
        /// <summary>
        /// Used to Delete Particular branch
        /// </summary>
        public void uspDeleteBranch()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspDeleteBranch";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            (new Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetAllBranch
        /// <summary>
        /// Used to get all Branch list
        /// </summary>
        /// <returns>returns the All Branch Data </returns>
        public DataTable uspGetAllBranch()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "CPV.uspGetAllBranch";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllBranchWithCompanyName
        /// <summary>
        /// Used to get all Branch list with its company name
        /// </summary>
        /// <param name="_UserID">UserId of the logged in user</param>
        /// <param name="_RoleID">RoleID of the logged in user</param>
        /// <returns>returns the Branch Data based on the user</returns>
        public DataTable uspGetAllBranchWithCompanyName(int _UserID , int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllBranchWithCompanyName";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetBranchByCompanyID
        /// <summary>
        /// Used to get the Branch List associated with the company
        /// </summary>
        /// <returns>returns the Table list of the Branches</returns>
        public DataTable uspGetBranchByCompanyID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetBranchByCompanyID";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetBranchByBranchID
        /// <summary>
        /// Used to get the Branch data of the particular branch
        /// </summary>
        /// <returns>returns the Data of the selected Branch</returns>
        public DataTable uspGetBranchByBranchID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetBranchByBranchID";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDateByBranchID
        /// <summary>
        /// Used to get the date by branch id
        /// </summary>
        /// <returns>returns the data based on branch id</returns>
        public DataTable uspGetDateByBranchID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDateByBranchID";
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID.ToString());
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspUpdateBranchStatus
        /// <summary>
        /// Updates the branch branch
        /// </summary>
        public int uspUpdateBranchStatus()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateBranchStatus";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@IsActive", this.IsActive);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

    }
}
