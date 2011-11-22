using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using System.Data;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class TaskeroUserBusinessImpl : ITaskeroUserBusiness
    {
        #region IBusTaskeroUser Members

        public TaskeroUser UserLogin(string UserName, string Password, string AllowedIP)
        {
            try
            {
                //========== Create Entity object =================//
                TaskeroUser ObjTaskeroUser = new TaskeroUser();
                Roles ObjRole = new Roles();                

                List<Permission> ObjRolePermissionList = new List<Permission>();
                List<Permission> ObjUserPermissionList = new List<Permission>();



                //========== Create data table object comes form store procedure=================//
                DataTable DTTaskeroUserDetail;
                DataTable DTUserRole = new DataTable();
                DataTable DTRolePermission = new DataTable();
                DataTable DTUserPermission = new DataTable();


                //========== Get data from data access=================//
                DataSet DSTaskeroUserLogin = DataAccessFactory.Instance.GetResult("[CPV].[uspGetCheckLogin]", new object[] { UserName, Password, AllowedIP });

                if (DSTaskeroUserLogin != null && DSTaskeroUserLogin.Tables.Count > 0 && DSTaskeroUserLogin.Tables[0].Rows.Count >0)
                {
                    //========== Store in to data table from dataset =================//
                    DTTaskeroUserDetail = DSTaskeroUserLogin.Tables[0];
                    DTUserRole = DSTaskeroUserLogin.Tables[1];
                    DTRolePermission = DSTaskeroUserLogin.Tables[2];
                    DTUserPermission = DSTaskeroUserLogin.Tables[3];

                    ObjTaskeroUser.TaskeroUserID = int.Parse(DTTaskeroUserDetail.Rows[0]["TaskeroUserID"].ToString());
                    ObjTaskeroUser.LoginID = DTTaskeroUserDetail.Rows[0]["LoginID"].ToString();
                    ObjTaskeroUser.LoginPass = DTTaskeroUserDetail.Rows[0]["LoginPass"].ToString();
                    ObjTaskeroUser.LastName = DTTaskeroUserDetail.Rows[0]["LastName"].ToString();
                    ObjTaskeroUser.FirstName = DTTaskeroUserDetail.Rows[0]["FirstName"].ToString();
                    ObjTaskeroUser.RoleID = int.Parse(DTTaskeroUserDetail.Rows[0]["RoleID"].ToString());
                    ObjTaskeroUser.AllowedIP = DTTaskeroUserDetail.Rows[0]["IPAccess"].ToString();

                    if (DTUserRole.Rows.Count > 0)
                    {
                        ObjRole.RoleID = int.Parse(DTUserRole.Rows[0]["RoleID"].ToString());
                        ObjRole.RoleDesc = DTUserRole.Rows[0]["RoleDesc"].ToString();
                        ObjRole.RoleLevel = int.Parse(DTUserRole.Rows[0]["RoleLevel"].ToString());
                        if (DTRolePermission.Rows.Count > 0)
                        {
                            foreach (DataRow dr in DTRolePermission.Rows)
                            {
                                Permission ObjPermission = new Permission();
                                ObjPermission.PermissionID = int.Parse(dr["PermissionID"].ToString());
                                ObjPermission.PermissionDesc = dr["PermissionDesc"].ToString();
                                ObjRolePermissionList.Add(ObjPermission);                                
                            }
                            ObjRole.RolePermission = ObjRolePermissionList;
                        }
                        ObjTaskeroUser.UserRoles = ObjRole;
                    }

                    if (DTUserPermission.Rows.Count > 0)
                    {
                        if (DTUserPermission.Rows.Count > 0)
                        {
                            foreach (DataRow dr in DTUserPermission.Rows)
                            {
                                Permission ObjPermission = new Permission();
                                ObjPermission.PermissionID = int.Parse(dr["PermissionID"].ToString());
                                ObjPermission.PermissionDesc = dr["PermissionDesc"].ToString();
                                ObjUserPermissionList.Add(ObjPermission);                                
                            }
                            ObjTaskeroUser.UserPermission = ObjUserPermissionList;
                        }                       
                    }

                }
                

                //return new TaskeroUser();

                return ObjTaskeroUser;
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        #endregion
    }
}
