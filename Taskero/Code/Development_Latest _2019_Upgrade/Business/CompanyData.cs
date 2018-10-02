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
    public class CompanyData : Company
    {

        #region uspInsertCompany
        /// <summary>
        /// Inserts new Company
        /// </summary>
        public void uspInsertCompany()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertCompany";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspUpdateCompany
        /// <summary>
        /// Updates a company
        /// </summary>
        public void uspUpdateCompany()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspUpdateCompany";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
            Cmd.Parameters.AddWithValue("@BrandID", this.BrandID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspDeleteCompany
        /// <summary>
        /// Delete a company
        /// </summary>
        public void uspDeleteCompany()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspDeleteCompany";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetAllCompany
        /// <summary>
        /// Get All company names
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllCompany()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllCompany";            
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCheckDuplicateCompany
        /// <summary>
        /// Checks Duplicate Company Names
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetCheckDuplicateCompany()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetCheckDuplicateCompany";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            Cmd.Parameters.AddWithValue("@CompanyName", this.CompanyName);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllCompanyByUserID
        /// <summary>
        /// Used to get all company list 
        /// </summary>
        /// <param name="_UserID">UserId of the logged in user</param>
        /// <param name="_RoleID">RoleID of the logged in user</param>
        /// <returns>returns the Branch Data based on the user</returns>
        public DataTable uspGetAllCompanyByUserID(int _UserID, int _RoleID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetAllCompanyByUserID";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", _UserID);
            Cmd.Parameters.AddWithValue("@RoleID", _RoleID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetDateByCompanyID
        /// <summary>
        /// Used to get the date by branch id
        /// </summary>
        /// <returns>returns the data based on branch id</returns>
        public DataTable uspGetDateByCompanyID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetDateByCompanyID";
            Cmd.Parameters.AddWithValue("@CompanyID", this.CompanyID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
