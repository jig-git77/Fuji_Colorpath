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
    public abstract class TempRHDevice
    {

        #region Variables
        private string _TableName = "TempRHDevice";
        private short _TempRHDeviceID = 0;
        private int _BranchID = 0;
        private string _TRHDeviceSerialNum = "";
        private short _TRHDeviceTypeID = 0;
        private decimal _TargetTemp = Convert.ToDecimal("0");
        private decimal _TargetRH = Convert.ToDecimal("0");
        private string _DeviceID = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public Int16 TempRHDeviceID
        {
            get { return _TempRHDeviceID; }
            set { _TempRHDeviceID = value; }
        }
        public string TRHDeviceSerialNum
        {
            get { return _TRHDeviceSerialNum; }
            set { _TRHDeviceSerialNum = value; }
        }
        public Int16 TRHDeviceTypeID
        {
            get { return _TRHDeviceTypeID; }
            set { _TRHDeviceTypeID = value; }
        }
        public decimal TargetTemp
        {
            get { return _TargetTemp; }
            set { _TargetTemp = value; }
        }
        public decimal TargetRH
        {
            get { return _TargetRH; }
            set { _TargetRH = value; }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public string Device
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        #endregion       
    }
}