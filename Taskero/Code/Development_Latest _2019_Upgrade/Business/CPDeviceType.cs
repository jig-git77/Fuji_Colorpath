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
    public abstract class CPDeviceType
    {

        #region Variables
        private string _TableName = "CPDeviceType";
        private short _CPDeviceTypeID = 0;
        private string _CPDeviceTypeDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short CPDeviceTypeID
        {
            get { return _CPDeviceTypeID; }
            set { _CPDeviceTypeID = value; }
        }
        public string CPDeviceTypeDesc
        {
            get { return _CPDeviceTypeDesc; }
            set { _CPDeviceTypeDesc = value; }
        }
        #endregion
    }
}