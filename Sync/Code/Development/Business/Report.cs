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
    public abstract class Report
    {

        #region Variables
        private string _TableName = "Report";
        private short _ReportID = 0;
        private string _ReportDesc = "";
        private short _ReportTypeID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        public string ReportDesc
        {
            get { return _ReportDesc; }
            set { _ReportDesc = value; }
        }
        public short ReportTypeID
        {
            get { return _ReportTypeID; }
            set { _ReportTypeID = value; }
        }
        #endregion

    }
}