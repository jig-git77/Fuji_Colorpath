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
    public abstract class UserLog
    {
        #region Variables
        private string _TableName = "UserLog";
        private int _ID = 0;
        private int _TaskeroUserID = 0;
        private string _IPAddress = "";
        private string _LoginTime = "";
        private string _LogoutTime = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        public string LoginTime
        {
            get { return _LoginTime; }
            set { _LoginTime = value; }
        }
        public string LogoutTime
        {
            get { return _LogoutTime; }
            set { _LogoutTime = value; }
        }
        #endregion
    }
}
