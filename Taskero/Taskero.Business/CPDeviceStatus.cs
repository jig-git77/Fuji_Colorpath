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
    public abstract class CPDeviceStatus
    {

        #region Variables
        private string _TableName = "CPDeviceStatus";
        private int _CPDeviceStatusID = 0;
        private int _CPDeviceID = 0;
        private short _CPDeviceStatusTypeID = 0;
        private bool _OperatorReset = true;
        private string _StatusDT = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPDeviceStatusID
        {
            get { return _CPDeviceStatusID; }
            set { _CPDeviceStatusID = value; }
        }
        public int CPDeviceID
        {
            get { return _CPDeviceID; }
            set { _CPDeviceID = value; }
        }
        public short CPDeviceStatusTypeID
        {
            get { return _CPDeviceStatusTypeID; }
            set { _CPDeviceStatusTypeID = value; }
        }
        public bool OperatorReset
        {
            get { return _OperatorReset; }
            set { _OperatorReset = value; }
        }
        public string StatusDT
        {
            get { return _StatusDT; }
            set { _StatusDT = value; }
        }
        #endregion     
    }
}