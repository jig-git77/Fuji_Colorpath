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
    public abstract class CPSyncPrintTarget
    {
        #region Variables
        private string _TableName = "PrintTarget";
        private short _PrintTargetID = 0;
        private short _PressRunID = 0;
        private int _PatchTypeID = 0;
        private int _ColorCharID = 0;
        private decimal _Target = 0;
        private decimal _ToleranceLimit = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PrintTargetID
        {
            get { return _PrintTargetID; }
            set { _PrintTargetID = value; }
        }
        public short PressRunID
        {
            get { return _PressRunID; }
            set { _PressRunID = value; }
        }

        public int PatchTypeID
        {
            get { return _PatchTypeID; }
            set { _PatchTypeID = value; }
        }
        public int ColorCharID
        {
            get { return _ColorCharID; }
            set { _ColorCharID = value; }
        }
        public decimal Target
        {
            get { return _Target; }
            set { _Target = value; }
        }
        public decimal ToleranceLimit
        {
            get { return _ToleranceLimit; }
            set { _ToleranceLimit = value; }
        }
        #endregion
    }
}
