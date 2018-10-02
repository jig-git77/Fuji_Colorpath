#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class RolePermissionData : RolePermission
    {
        #region Inserting and Updating the RolePermission 
        /// <summary>
        /// Inserting taskero user.
        /// </summary>
        /// <returns>returns the result value of operation</returns>
        public int uspInsertRolePermission()
        {
            //========== Execute Command ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspInsertRolePermission";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@RoleManagement", this.RoleManagement);
            Cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Int, 2, ParameterDirection.Output, false, 0, 0, null, DataRowVersion.Default, null));
            (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);
            return Convert.ToInt32(Cmd.Parameters["@Result"].Value.ToString());
        }        
        #endregion
    }

}
