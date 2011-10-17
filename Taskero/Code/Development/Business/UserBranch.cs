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
    public abstract class UserBranch
    {

        #region Variables
        private string _TableName = "UserBranch";
        private short _UserBranchID = 0;
        private int _TaskeroUserID = 0;
        private int _BranchID = 0;
        private int _CompanyID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short UserBranchID
        {
            get { return _UserBranchID; }
            set { _UserBranchID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        #endregion       

    }
}