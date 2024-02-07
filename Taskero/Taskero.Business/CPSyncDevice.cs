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
     public abstract class CPSyncDevice
    {
       
        private int _BranchID = 0;
        private int _CPDongleID = 0;
        private int _CPSyncDeviceID = 0;
        private string _TableName = "CPSyncDevice";
        private string _CPSyncDeviceName = "";
        private bool _isUsed = false;
        private string _CPSyncDeviceSerialNum = "";

        public string TableName
        {
            get { return _TableName; }
            set { }
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

        public string CPSyncDeviceName
        {
            get { return _CPSyncDeviceName; }
            set { _CPSyncDeviceName = value; }
        }

        public int CPSyncDeviceID
        {
            get { return _CPSyncDeviceID; }
            set { _CPSyncDeviceID = value; }
        }

        public  bool isUsed
        {
            get { return _isUsed; }
            set { _isUsed = value; }
        }

        public string CPSyncDeviceSerialNum
        {
            get { return _CPSyncDeviceSerialNum; }
            set { _CPSyncDeviceSerialNum = value; }
        }

    }
}
