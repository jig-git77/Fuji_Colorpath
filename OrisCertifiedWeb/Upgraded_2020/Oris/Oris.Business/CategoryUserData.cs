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
    public class CategoryUserData : CategoryUser
    {
        #region uspInsertCategoryUser
        /// <summary>
        /// Used to insert New Category
        /// </summary>
        public int uspInsertCategoryUser(string TaskeroUserIDs)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            //Cmd.CommandText = "CPV.uspInsertCategoryUser";
            Cmd.CommandText = "Video.uspInsertCategoryUser";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@TaskeroUserIDs", TaskeroUserIDs);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspInsertCategoryBranchUser
        /// <summary>
        /// Used to insert New Category
        /// </summary>
        public int uspInsertCategoryBranchUser(string TaskeroUserIDs, int BranchID)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            //Cmd.CommandText = "CPV.uspInsertCategoryBranchUser";
            Cmd.CommandText = "Video.uspInsertCategoryBranchUser";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", BranchID);
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@TaskeroUserIDs", TaskeroUserIDs);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspGetUserOfBranchID
        /// <summary>
        /// Get User of BranchID
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetUserOfBranchID(string BranchIDs, int TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "CPV.uspGetUserOfBranchID";
            //Cmd.CommandText = "Video.uspGetUserOfBranchID";
            Cmd.Parameters.AddWithValue("@SelectedIDs", BranchIDs);
            Cmd.Parameters.AddWithValue("@TaskeroUserID", TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetAllCategoryUserByCategoryID
        /// <summary>
        /// Get Category User By CategoryID
        /// </summary>
        /// <returns></returns>
        public DataSet uspGetAllCategoryUserByCategoryID()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllCategoryUserByCategoryID";
            Cmd.CommandText = "Video.uspGetAllCategoryUserByCategoryID";
            Cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspGetAllCategoryUserByBranchID
        /// <summary>
        /// Get Category User By BranchID
        /// </summary>
        /// <returns></returns>
        public DataSet uspGetAllCategoryUserByBranchID(int BranchID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllCategoryUserByBranchID";
            Cmd.CommandText = "Video.uspGetAllCategoryUserByBranchID";
            Cmd.Parameters.AddWithValue("@BranchID", BranchID);
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion

        #region uspDeleteCategoryBranchUser
        /// <summary>
        /// Delete Category User
        /// </summary>
        public int uspDeleteCategoryBranchUser(int BranchID)
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            //Cmd.CommandText = "CPV.uspDeleteCategoryBranchUser";
            Cmd.CommandText = "Video.uspDeleteCategoryBranchUser";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@BranchID", BranchID);
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion
    }
}
