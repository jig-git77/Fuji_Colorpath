#region System
using System;
using System.Collections.Generic;
using System.Text;
using Fuji.DataAccess;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace Fuji.Business
{
    public class RolesData : Roles
    {
        #region Retieving the Permission
        /// <summary>
        /// Retrieving the Roles
        /// </summary>
        /// <returns>returns the permission data</returns>
        public System.Collections.ArrayList uspRetrievePermission()
        {
            //========== Execute Command with SqlDataReader ==========//
            SqlCommand Cmd = new SqlCommand();
            System.Collections.ArrayList arrResult = new System.Collections.ArrayList();

            Cmd.CommandText = "CPV.uspGetRoles";
            Cmd.CommandType = CommandType.StoredProcedure;
            //Cmd.Parameters.AddWithValue("@RoleLevel", Convert.ToInt16(Fuji.Common.Sessions.RoleLevel));
            Cmd.Parameters.AddWithValue("@TaskeroUserID", Fuji.Common.Sessions.UserID);            
            IDataReader IDR = (new Fuji.DataAccess.Database()).ExecuteDataReader(Cmd);// TPSDataAccess.TPSDADatabase.ExecuteDataReader(cmd, TPSDataAccess.TPSDBCSTRING.GetCSTRING(TPSDataAccess.TPSDBCSTRING.DBASE.TPSS))
            if (IDR == null)
            {
                if (Cmd.Connection.State == ConnectionState.Open)
                {
                    Cmd.Connection.Close();
                }
                return arrResult;
            }
            RolesData ObjRls;
            while (IDR.Read())
            {
                ObjRls = new RolesData();
                ObjRls.RoleID = Convert.ToInt16(IDR[0]);
                ObjRls.RoleDesc = IDR[1].ToString();
                arrResult.Add(ObjRls);
            }
            if (Cmd.Connection.State == ConnectionState.Open)
            {
                Cmd.Connection.Close();
            }
            return arrResult;
            
        }
        #endregion

        #region Retieving the Roles Data
        /// <summary>
        /// Retrieving the Roles Management Data
        /// </summary>
        /// <returns>returns the role management data</returns>
        public DataSet uspGetRoleManagementData()
        {
            //========== Execute Command with SqlDataReader ==========//
            SqlCommand Cmd = new SqlCommand();           

            Cmd.CommandText = "CPV.uspGetRoleManagementData";
            Cmd.CommandType = CommandType.StoredProcedure;
            return (new DataAccess.Database()).GetDataSet(Cmd);
        }
        #endregion
    }
}
