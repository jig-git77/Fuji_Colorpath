#region System
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace Fuji.Business
{
    public class PermissionData : Permission
    {
        #region Retieving the Permission
        /// <summary>
        /// Retrieves the permissions according to the roles.
        /// </summary>
        /// <returns>returns the permissions by role id of the user</returns>
        public DataSet uspGetRetrievePermission()
        {
            //========== Execute Command with Dataset ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetRetrievePermission";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@RoleID", this.PermissionID);
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);

        }
        #endregion

        #region Retieving the Permission Table for UserManagement
        /// <summary>
        /// Retieving the Permission Table for UserManagement
        /// </summary>
        /// <returns>returns the permissions by role id of the user</returns>
        public DataSet uspGetRetrievePermissionUserManagement()
        {
            //========== Execute Command with Dataset ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "CPV.uspGetRetrievePermissionUserManagement";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@RoleID", Convert.ToInt16(Fuji.Common.Sessions.RoleID));
            return (new Fuji.DataAccess.Database()).GetDataSet(Cmd);

        }
        #endregion
    }
}
