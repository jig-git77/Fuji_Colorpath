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
    public abstract class CPDevicePrintTarget
    {

        #region Variables
        private string _TableName = "CPDevicePrintTarget";
        private int _CPDevicePrintTargetID = 0;
        private int _DeviceID = 0;
        private string _PrintTarget = "";
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPDevicePrintTargetID
        {
            get { return _CPDevicePrintTargetID; }
            set { _CPDevicePrintTargetID = value; }
        }
        public int DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }
        public string PrintTarget
        {
            get { return _PrintTarget; }
            set { _PrintTarget = value; }
        }
        #endregion

       
    }
}