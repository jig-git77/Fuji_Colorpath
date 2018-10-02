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
    public abstract class Permission
    {

        #region Variables
        private string _TableName = "Permission";
        private short _PermissionID = 0;
        private string _PermissionDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        public string PermissionDesc
        {
            get { return _PermissionDesc; }
            set { _PermissionDesc = value; }
        }
        #endregion

    }
}