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
    public abstract class PressType
    {
        #region Variables
        private string _TableName = "PressType";
        private short _PressTypeID = 0;
        private string _PressType = "";
        private string _PressTypeDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PressTypeID
        {
            get { return _PressTypeID; }
            set { _PressTypeID = value; }
        }
        public string PressTypeName
        {
            get { return _PressType; }
            set { _PressType = value; }
        }
        public string PressTypeDesc
        {
            get { return _PressTypeDesc; }
            set { _PressTypeDesc = value; }
        }
        #endregion

    }
}
