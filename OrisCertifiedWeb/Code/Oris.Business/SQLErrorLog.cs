#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Oris.DataAccess;
#endregion

namespace Oris.Business
{
    public abstract class SQLErrorLog
    {

        #region Variables
        private string _TableName = "SQLErrorLog";
        private int _ErrorLogID = 0;
        private string _ErrorTime = "";
        private string _UserName = "";
        private int _ErrorNumber = 0;
        private int _ErrorSeverity = 0;
        private int _ErrorState = 0;
        private string _ErrorProcedure = "";
        private int _ErrorLine = 0;
        private string _ErrorMessage = "";
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
        public string ErrorTime
        {
            get { return _ErrorTime; }
            set { _ErrorTime = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public int ErrorNumber
        {
            get { return _ErrorNumber; }
            set { _ErrorNumber = value; }
        }
        public int ErrorSeverity
        {
            get { return _ErrorSeverity; }
            set { _ErrorSeverity = value; }
        }
        public int ErrorState
        {
            get { return _ErrorState; }
            set { _ErrorState = value; }
        }
        public string ErrorProcedure
        {
            get { return _ErrorProcedure; }
            set { _ErrorProcedure = value; }
        }
        public int ErrorLine
        {
            get { return _ErrorLine; }
            set { _ErrorLine = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        #endregion      

    }
}