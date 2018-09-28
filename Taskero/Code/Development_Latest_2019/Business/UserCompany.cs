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
    public abstract class UserCompany
    {

        #region Variables
        private string _TableName = "UserCompany";
        private short _UserCompanyID = 0;
        private int _TaskeroUserID = 0;
        private int _CompanyID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short UserCompanyID
        {
            get { return _UserCompanyID; }
            set { _UserCompanyID = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        #endregion      

    }
}