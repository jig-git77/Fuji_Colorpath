using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using System.Data;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class TaskeroUserBusinessImpl : ITaskeroUserBusiness
    {
        #region IBusTaskeroUser Members

        public TaskeroUser UserLogin(string userName, string password, string allowedIp)
        {
            //========== Create Entity object =================//
            var objTaskeroUser = new TaskeroUser();
            var objRole = new Roles();                

            var objRolePermissionList = new List<Permission>();
            var objUserPermissionList = new List<Permission>();



            //========== Create data table object comes form store procedure=================//
            DataTable dtTaskeroUserDetail;
            DataTable dtUserRole;
            DataTable dtRolePermission;
            DataTable dtUserPermission;


            //========== Get data from data access=================//
            var dsTaskeroUserLogin = DataAccessFactory.Instance.GetResult("[CPV].[uspGetCheckLogin]", new object[] { userName, password, allowedIp });

            if (dsTaskeroUserLogin != null && dsTaskeroUserLogin.Tables.Count > 0 && dsTaskeroUserLogin.Tables[0].Rows.Count >0)
            {
                //========== Store in to data table from dataset =================//
                dtTaskeroUserDetail = dsTaskeroUserLogin.Tables[0];
                dtUserRole = dsTaskeroUserLogin.Tables[1];
                dtRolePermission = dsTaskeroUserLogin.Tables[2];
                dtUserPermission = dsTaskeroUserLogin.Tables[3];

                objTaskeroUser.TaskeroUserID = int.Parse(dtTaskeroUserDetail.Rows[0]["TaskeroUserID"].ToString());
                objTaskeroUser.LoginID = dtTaskeroUserDetail.Rows[0]["LoginID"].ToString();
                objTaskeroUser.LoginPass = dtTaskeroUserDetail.Rows[0]["LoginPass"].ToString();
                objTaskeroUser.LastName = dtTaskeroUserDetail.Rows[0]["LastName"].ToString();
                objTaskeroUser.FirstName = dtTaskeroUserDetail.Rows[0]["FirstName"].ToString();
                objTaskeroUser.RoleID = int.Parse(dtTaskeroUserDetail.Rows[0]["RoleID"].ToString());
                objTaskeroUser.AllowedIP = dtTaskeroUserDetail.Rows[0]["IPAccess"].ToString();

                if (dtUserRole.Rows.Count > 0)
                {
                    objRole.RoleID = int.Parse(dtUserRole.Rows[0]["RoleID"].ToString());
                    objRole.RoleDesc = dtUserRole.Rows[0]["RoleDesc"].ToString();
                    objRole.RoleLevel = int.Parse(dtUserRole.Rows[0]["RoleLevel"].ToString());
                    if (dtRolePermission.Rows.Count > 0)
                    {
                        objRolePermissionList.AddRange(from DataRow dr in dtRolePermission.Rows
                                                       select new Permission {PermissionID = int.Parse(dr["PermissionID"].ToString()), PermissionDesc = dr["PermissionDesc"].ToString()});
                        objRole.RolePermission = objRolePermissionList;
                    }
                    objTaskeroUser.UserRoles = objRole;
                }

                if (dtUserPermission.Rows.Count > 0)
                {
                    if (dtUserPermission.Rows.Count > 0)
                    {
                        objUserPermissionList.AddRange(from DataRow dr in dtUserPermission.Rows
                                                       select new Permission {PermissionID = int.Parse(dr["PermissionID"].ToString()), PermissionDesc = dr["PermissionDesc"].ToString()});
                        objTaskeroUser.UserPermission = objUserPermissionList;
                    }
                }

            }
                

            //return new TaskeroUser();

            return objTaskeroUser;
        }

        #endregion
    }
}
