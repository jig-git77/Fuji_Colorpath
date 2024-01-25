#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion


namespace Taskero.Business
{
    public class TaskeroUserData : TaskeroUser
    {
        #region uspGetCheckLogin
        /// <summary>
        /// Check Login using username and password.
        /// </summary>
        /// <returns>returns the login information</returns>
        public DataSet uspGetCheckLogin()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetCheckLogin";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            Cmd.Parameters.AddWithValue("@LoginPass", this.LoginPass);
            Cmd.Parameters.AddWithValue("@AllowedIP", this.AllowedIP);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetRetrievePassword
        /// <summary>
        /// Gets the password using email and username.
        /// </summary>
        /// <returns>returns the password </returns>
        public DataTable uspGetRetrievePassword()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetRetrievePassword";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            Cmd.Parameters.AddWithValue("@Email", this.Email);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetTaskeroUser
        /// <summary>
        /// Gets the taskero users per role id.
        /// </summary>
        /// <returns>returns the information of the user</returns>
        public DataTable uspGetTaskeroUser()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetTaskeroUser";
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetUserByRoleID
        /// <summary>
        /// Gets the taskero users by role id.
        /// </summary>
        /// <returns>returns the User by role id</returns>
        public DataTable uspGetUserByRoleID()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetUserByRoleID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetTaskeroUserPassingRoleID
        /// <summary>
        /// Gets the taskero support users per role id.
        /// </summary>
        /// <returns>returns the information of the support user</returns>
        public DataTable uspGetTaskeroUserPassingRoleID()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetTaskeroUserPassingRoleID";
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetUserOfAssignedCompany
        /// <summary>
        /// Gets the taskero users by role id and company which they assigned.
        /// </summary>
        /// <returns>returns users which are assigned to that company</returns>
        public DataTable uspGetUserOfAssignedCompany()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetUserOfAssignedCompany";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetTaskeroUserAndDetails
        /// <summary>
        /// Gets the taskero users data and its details Data
        /// </summary>
        /// <returns>returns the taskero user and its details</returns>
        public DataSet uspGetTaskeroUserAndDetails()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetTaskeroUserAndDetails";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspTaskeroUserDataByUsername
        /// <summary>
        /// Gets the user information using the username.
        /// </summary>
        /// <returns>returns the taskero user data by user name</returns>
        public DataSet uspTaskeroUserDataByUsername()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspTaskeroUserDataByUsername";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region Inserting the Taskero User

        #region uspInsertTaskeroUser
        /// <summary>
        /// Inserting taskero user.
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspInsertTaskeroUser()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertTaskeroUser";
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);

            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            Cmd.Parameters.AddWithValue("@LoginPass", this.LoginPass);
            Cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
            Cmd.Parameters.AddWithValue("@LastName", this.LastName);
            Cmd.Parameters.AddWithValue("@Email", this.Email);
            Cmd.Parameters.AddWithValue("@IsActive", this.IsActive);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@PermissionID", this.PermissionID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@ReportID", this.ReportID);
            Cmd.Parameters.AddWithValue("@Customer", this.Customer);
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            Cmd.Parameters.AddWithValue("@AllowedIP", this.AllowedIP);

            Cmd.Parameters.AddWithValue("@EmailWhenYellow", this.EmailWhenYellow);
            Cmd.Parameters.AddWithValue("@EmailWhenRed", this.EmailWhenRed);

            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspUpdateTaskeroUser
        /// <summary>
        /// USed to update taskero user
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspUpdateTaskeroUser()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateTaskeroUser";
            Cmd.CommandType = CommandType.StoredProcedure;

            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            Cmd.Parameters.AddWithValue("@LoginPass", this.LoginPass);
            Cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
            Cmd.Parameters.AddWithValue("@LastName", this.LastName);
            Cmd.Parameters.AddWithValue("@Email", this.Email);
            Cmd.Parameters.AddWithValue("@IsActive", this.IsActive);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@PermissionID", this.PermissionID);
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@DongleID", this.DongleID);
            Cmd.Parameters.AddWithValue("@ReportID", this.ReportID);
            Cmd.Parameters.AddWithValue("@Customer", this.Customer);
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            Cmd.Parameters.AddWithValue("@AllowedIP", this.AllowedIP);

            Cmd.Parameters.AddWithValue("@EmailWhenYellow", this.EmailWhenYellow);
            Cmd.Parameters.AddWithValue("@EmailWhenRed", this.EmailWhenRed);
            Cmd.Parameters.AddWithValue("@SiteLicenseAgreed", this.SiteLicenseAgreed);

            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #endregion

        #region Update MyAccount Taskero User
        /// <summary>
        /// Update MyAccount Taskero User
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspUpdateMyAccountTaskeroUser()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspUpdateMyAccountTaskeroUser";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@LoginPass", this.LoginPass);
            Cmd.Parameters.AddWithValue("@FirstName", this.FirstName);
            Cmd.Parameters.AddWithValue("@LastName", this.LastName);
            Cmd.Parameters.AddWithValue("@Email", this.Email);
            Cmd.Parameters.AddWithValue("@LastModified_UserID", this.LastModified_UserID);
            Cmd.Parameters.AddWithValue("@Customer", this.Customer);
            Cmd.Parameters.AddWithValue("@AllowedIP", this.AllowedIP);

            Cmd.Parameters.AddWithValue("@EmailWhenYellow", this.EmailWhenYellow);
            Cmd.Parameters.AddWithValue("@EmailWhenRed", this.EmailWhenRed);

            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspGetTaskeroUserDataByUserName
        /// <summary>
        /// Deletes the Tasker User
        /// </summary>
        /// <returns>returns the result of the last operation</returns>
        public int uspDeleteTaskeroUserAndRelations()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspDeleteTaskeroUserAndRelations";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@LoginID", this.LoginID);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspGetSupportUserWithDongleByBranchID
        /// <summary>
        /// Gets the taskero support users and dongle by branchid as per logged in user role id.
        /// </summary>
        /// <param name="_IntUserRoleID">Role id of the user to be fetch</param>
        /// <returns>returns the information of the support user with dongle</returns>
        public DataSet uspGetSupportUserWithDongleByBranchID(int _IntUserRoleID)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetSupportUserWithDongleByBranchID";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", this.BranchID);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", this.TaskeroUserID);
            Cmd.Parameters.AddWithValue("@RoleID", this.RoleID);
            Cmd.Parameters.AddWithValue("@UserRoleID", _IntUserRoleID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
    }
}
