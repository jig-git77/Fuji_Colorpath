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
    public abstract class TempRHDeviceType
    {

        #region Variables
        private string _TableName = "TempRHDeviceType";
        private short _TRHDeviceTypeID = 0;
        private string _TRHDeviceTypeDesc = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short TRHDeviceTypeID
        {
            get { return _TRHDeviceTypeID; }
            set { _TRHDeviceTypeID = value; }
        }
        public string TRHDeviceTypeDesc
        {
            get { return _TRHDeviceTypeDesc; }
            set { _TRHDeviceTypeDesc = value; }
        }
        #endregion       
    }
}