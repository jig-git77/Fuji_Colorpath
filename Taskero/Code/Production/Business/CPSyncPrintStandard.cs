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
    public abstract class CPSyncPrintStandard
    {
        #region Variables
        private string _TableName = "PrintStandard";
        private short _PrintStandardID = 0;
        private short _PrintStandardTypeID = 0;
        private int _ColorCharID = 0;
        private int _PatchTypeID = 0;
        private decimal _Target = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PrintStandardID
        {
            get { return _PrintStandardID; }
            set { _PrintStandardID = value; }
        }
        public short PrintStandardTypeID
        {
            get { return _PrintStandardTypeID; }
            set { _PrintStandardTypeID = value; }
        }
        public int ColorCharID
        {
            get { return _ColorCharID; }
            set { _ColorCharID = value; }
        }
        public int PatchTypeID
        {
            get { return _PatchTypeID; }
            set { _PatchTypeID = value; }
        }
        public decimal Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        #endregion
    }
}
