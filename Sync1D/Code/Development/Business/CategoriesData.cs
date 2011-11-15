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
    public class CategoriesData : Categories
    {
        #region uspInsertCategory
        /// <summary>
        /// Used to insert New Category
        /// </summary>
        public int uspInsertCategory()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "Video.uspInsertCategory";
            //Cmd.CommandText = "CPV.uspInsertCategory";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@Description", this.Description);
            Cmd.Parameters.AddWithValue("@ParentID", this.ParentID);
            Cmd.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
            Cmd.Parameters.AddWithValue("@ModifiedBy", this.ModifiedBy);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspUpdateCategory
        /// <summary>
        /// Used to Update Existing Category
        /// </summary>
        public int uspUpdateCategory()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspUpdateCategory";
            Cmd.CommandText = "Video.uspUpdateCategory";
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            Cmd.Parameters.AddWithValue("@Description", this.Description);
            Cmd.Parameters.AddWithValue("@ParentID", this.ParentID);
            Cmd.Parameters.AddWithValue("@ModifiedBy", this.ModifiedBy);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }
        #endregion

        #region uspDeleteCategory
        /// <summary>
        /// Used to Delete Particular Category
        /// </summary>
        public void uspDeleteCategory()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspDeleteCategory";
            Cmd.CommandText = "Video.uspDeleteCategory";
            Cmd.Parameters.AddWithValue("@CategoryID", this.CategoryID);
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
        }
        #endregion

        #region uspGetAllCategories
        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetAllCategories()
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetAllCategories";
            Cmd.CommandText = "Video.uspGetAllCategories";
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspGetCategoriesByUser
        /// <summary>
        /// Get Categories By User
        /// </summary>
        /// <returns></returns>
        public DataTable uspGetCategoriesByUser(int TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspGetCategoriesByUser";
            Cmd.CommandText = "Video.uspGetCategoriesByUser";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion

        #region uspCountAllCategoriesByUser
        /// <summary>
        /// Count Categories By User
        /// </summary>
        /// <returns></returns>
        public DataTable uspCountAllCategoriesByUser(int TaskeroUserID)
        {
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.CommandText = "CPV.uspCountAllCategoriesByUser";
            Cmd.CommandText = "Video.uspCountAllCategoriesByUser";
            Cmd.Parameters.AddWithValue("@TaskeroUserID", TaskeroUserID);
            return (new DataAccess.Database()).GetDataTable(Cmd);
        }
        #endregion
    }
}
