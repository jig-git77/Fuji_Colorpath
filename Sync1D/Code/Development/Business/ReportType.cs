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
    public abstract class ReportType
    {

        #region Variables
        private string _TableName = "ReportType";
        private short _ReportTypeID = 0;
        private string _ReportTypeDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short ReportTypeID
        {
            get { return _ReportTypeID; }
            set { _ReportTypeID = value; }
        }
        public string ReportTypeDesc
        {
            get { return _ReportTypeDesc; }
            set { _ReportTypeDesc = value; }
        }
        #endregion
    }
}