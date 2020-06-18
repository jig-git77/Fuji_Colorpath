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
    public abstract class CPSyncMeasurementDevice
    {
        private string _TableName = "CPSync.MeasurementDevice";
        private int _CPSyncMeasurementDeviceID = 0;
        private int _CPDongleID = 0;
        private int _BranchID = 0;
        private string _SerialNumber = "";
        private int _DeviceTypeID = 0;
        private bool _isActive = false;
        private int _PressID = 0;
      

        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPSyncMeasurementDeviceID
        {
            get { return _CPSyncMeasurementDeviceID; }
            set { _CPSyncMeasurementDeviceID = value; }
        }

        public int CPDongleID
        {
            get { return _CPDongleID; }
            set { _CPDongleID = value; }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }
        public int DeviceTypeID
        {
            get { return _DeviceTypeID; }
            set { _DeviceTypeID = value; }
        }

        public bool isActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public int PressID
        {
            get { return _PressID; }
            set { _PressID = value; }
        }

      
    }
}
