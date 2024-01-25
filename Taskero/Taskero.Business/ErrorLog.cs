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
    public abstract class ErrorLog
    {

        #region Variables
        private string _TableName = "ErrorLog";
        private int _ErrorLogID = 0;
        private string _TimeStamp = "";
        private string _ExType = "";
        private string _ExMessage = "";
        private string _ExStackTracke = "";
        private int _TaskeroUserID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int ErrorLogID
        {
            get { return _ErrorLogID; }
            set { _ErrorLogID = value; }
        }
        public string TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }
        public string ExType
        {
            get { return _ExType; }
            set { _ExType = value; }
        }
        public string ExMessage
        {
            get { return _ExMessage; }
            set { _ExMessage = value; }
        }
        public string ExStackTracke
        {
            get { return _ExStackTracke; }
            set { _ExStackTracke = value; }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        #endregion

    }
}