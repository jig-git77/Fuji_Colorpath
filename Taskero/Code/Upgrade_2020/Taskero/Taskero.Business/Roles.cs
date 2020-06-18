#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Taskero.DataAccess;
#endregion

namespace Taskero.Business
{
    public abstract class Roles
    {

        #region Variables
        private string _TableName = "Roles";
        private short _RoleID = 0;
        private string _RoleDesc = "";
        private short _RoleLevel = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public string RoleDesc
        {
            get { return _RoleDesc; }
            set { _RoleDesc = value; }
        }
        public short RoleLevel
        {
            get { return _RoleLevel; }
            set { _RoleLevel = value; }
        }
        #endregion
    }
}