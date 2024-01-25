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
    public abstract class Company
    {

        #region Variables
        private string _TableName = "Company";
        private int _CompanyID = 0;
        private string _CompanyName = "";
        private int _BrandID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        public int BrandID
        {
            get { return _BrandID; }
            set { _BrandID = value; }
        }
        #endregion
    }
}