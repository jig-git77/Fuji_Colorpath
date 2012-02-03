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
    public abstract class RolePermission
    {

        #region Variables
        private string _TableName = "RolePermission";
        private short _RolePermissionID = 0;
        private short _RoleID = 0;
        private short _PermissionID = 0;
        private string _RoleManagement = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short RolePermissionID
        {
            get { return _RolePermissionID; }
            set { _RolePermissionID = value; }
        }
        public short RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public short PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        public string RoleManagement
        {
            get { return _RoleManagement; }
            set { _RoleManagement = value; }
        }
        #endregion       
    }
}