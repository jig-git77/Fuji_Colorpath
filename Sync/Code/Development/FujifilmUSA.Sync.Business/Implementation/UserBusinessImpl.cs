using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;


namespace FujifilmUSA.Sync.Business.Implementation
{
    public class UserBusinessImpl : IUserBusiness
    {
        #region IUser Members

        public User UserLogin(string userName, string password, string allowedIp)
        {
            //========== Create Entity object =================//
            var objTaskeroUser = new User();

            Common.ImplementBusinessLogic(() =>
            {
                //var objRole = new Roles();

                var objRolePermissionList = new List<Permission>();
                var objUserPermissionList = new List<Permission>();

                //========== Create data table object comes form store procedure=================//
                DataTable dtTaskeroUserDetail;
                DataTable dtUserRole;
                DataTable dtRolePermission;
                DataTable dtUserPermission;


                //========== Get data from data access=================//
                var dsTaskeroUserLogin = DataAccessFactory.Instance.GetDsResult("[CPV].[uspGetCheckLogin]", new object[] { userName, password, allowedIp });

                if (dsTaskeroUserLogin != null && dsTaskeroUserLogin.Tables.Count > 0 && dsTaskeroUserLogin.Tables[0].Rows.Count > 0)
                {
                    //========== Store in to data table from dataset =================//
                    dtTaskeroUserDetail = dsTaskeroUserLogin.Tables[0];
                    dtUserRole = dsTaskeroUserLogin.Tables[1];
                    dtRolePermission = dsTaskeroUserLogin.Tables[2];
                    dtUserPermission = dsTaskeroUserLogin.Tables[3];

                    objTaskeroUser.UserId = int.Parse(dtTaskeroUserDetail.Rows[0]["TaskeroUserID"].ToString());
                    objTaskeroUser.LoginId = dtTaskeroUserDetail.Rows[0]["LoginID"].ToString();
                    objTaskeroUser.LoginPass = dtTaskeroUserDetail.Rows[0]["LoginPass"].ToString();
                    objTaskeroUser.LastName = dtTaskeroUserDetail.Rows[0]["LastName"].ToString();
                    objTaskeroUser.FirstName = dtTaskeroUserDetail.Rows[0]["FirstName"].ToString();
                    //objTaskeroUser.RoleId = int.Parse(dtTaskeroUserDetail.Rows[0]["RoleID"].ToString());
                    //objTaskeroUser.AllowedIp = dtTaskeroUserDetail.Rows[0]["IPAccess"].ToString();

                    //if (dtUserRole.Rows.Count > 0)
                    //{
                    //    objRole.RoleId = int.Parse(dtUserRole.Rows[0]["RoleID"].ToString());
                    //    objRole.RoleDesc = dtUserRole.Rows[0]["RoleDesc"].ToString();
                    //    objRole.RoleLevel = int.Parse(dtUserRole.Rows[0]["RoleLevel"].ToString());
                    //    if (dtRolePermission.Rows.Count > 0)
                    //    {
                    //        objRolePermissionList.AddRange(from DataRow dr in dtRolePermission.Rows
                    //                                       select new Permission { PermissionId = int.Parse(dr["PermissionID"].ToString()), PermissionDesc = dr["PermissionDesc"].ToString() });
                    //        objRole.RolePermission = objRolePermissionList;
                    //    }
                    //    objTaskeroUser.UserRoles = objRole;
                    //}

                    //if (dtUserPermission.Rows.Count > 0)
                    //{
                    //    if (dtUserPermission.Rows.Count > 0)
                    //    {
                    //        objUserPermissionList.AddRange(from DataRow dr in dtUserPermission.Rows
                    //                                       select new Permission { PermissionId = int.Parse(dr["PermissionID"].ToString()), PermissionDesc = dr["PermissionDesc"].ToString() });
                    //        objTaskeroUser.UserPermission = objUserPermissionList;
                    //    }
                    //}

                }
                //return new TaskeroUser();
            }, this, ErrorOccured);

            return objTaskeroUser;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
