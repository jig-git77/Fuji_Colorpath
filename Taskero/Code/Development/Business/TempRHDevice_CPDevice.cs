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
    public abstract class TempRHDevice_CPDevice
    {

        #region Variables
        private string _TableName = "TempRHDevice_CPDevice";
        private int _TempRHDevice_CPDeviceID = 0;
        private int _CPDeviceID = 0;
        private short _TempRHDeviceID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int TempRHDevice_CPDeviceID
        {
            get { return _TempRHDevice_CPDeviceID; }
            set { _TempRHDevice_CPDeviceID = value; }
        }
        public int CPDeviceID
        {
            get { return _CPDeviceID; }
            set { _CPDeviceID = value; }
        }
        public short TempRHDeviceID
        {
            get { return _TempRHDeviceID; }
            set { _TempRHDeviceID = value; }
        }
        #endregion
       
    }
}