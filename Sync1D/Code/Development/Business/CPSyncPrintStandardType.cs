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
    public abstract class CPSyncPrintStandardType
    {
        #region Variables
        private string _TableName = "PrintStandardType";
        private short _PrintStandardTypeID = 0;
        private string _PrintStandard = "";
        
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PrintStandardTypeID
        {
            get { return _PrintStandardTypeID; }
            set { _PrintStandardTypeID = value; }
        }

        public string PrintStandard
        {
            get { return _PrintStandard; }
            set { _PrintStandard = value; }
        }

        #endregion
    }
}
