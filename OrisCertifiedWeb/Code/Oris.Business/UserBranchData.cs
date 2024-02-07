#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public class UserBranchData : UserBranch
    {
        #region uspGetUserBranchData
        /// <summary>
        /// Gets the UserBranch by using UserID
        /// </summary>
        /// <returns>returns the User Branch Data</returns>
        public DataTable uspGetUserBranchData()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetUserBranchData";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetBranchOfCompanyByUserID
        /// <summary>
        /// Gets the company by using UserID
        /// </summary>
        /// <returns>returns the Branch Data</returns>
        public DataTable uspGetBranchOfCompanyByUserID(int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetBranchOfCompanyByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAssignSupportUserOfBranchID
        /// <summary>
        /// Gets the Support User using by BranchID
        /// </summary>
        /// <returns>returns the Support user of branch data</returns>
        public DataTable uspGetAssignSupportUserOfBranchID(int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAssignSupportUserOfBranchID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetBranchOfCompanyByUserIDAndCompanyID
        /// <summary>
        /// Gets the branch by selected company 
        /// </summary>
        /// <returns>returns the Branch Data</returns>
        public DataTable uspGetBranchOfCompanyByUserIDAndCompanyID(int _RoleID,string _CompanyID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetBranchOfCompanyByUserIDAndCompanyID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            Cmd.Parameters.AddWithValue("@CompanyID", _CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region Inserting the User Branches
        /// <summary>
        /// Inserting the User Branches.
        /// </summary>
        /// <param name="_SelectedIDs">selected id of the list</param>
        /// <param name="_NonSelectedIDs">non selected id of the list</param>
        /// <returns>returns the int value as a result</returns>
        public int uspInsertUserBranchData(string _SelectedIDs, string _NonSelectedIDs)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertUserBranchData";
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@SelectedIDs", _SelectedIDs);
            Cmd.Parameters.AddWithValue("@NonSelectedIDs", _NonSelectedIDs);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion
    }
}
