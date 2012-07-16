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
    public abstract class CPDevice
    {

        #region Variables
        private string _TableName = "CPDevice";
        private int _CPDeviceID = 0;
        private int _CPDongleID = 0;
        private int _CompanyID = 0;
        private int _BranchID = 0;
        private string _CPDeviceDesc = "";
        private short _MeasurementTypeID = 0;
        private short _CPDeviceTypeID = 0;
        private int _LastMeasurementID = 0;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int CPDeviceID
        {
            get { return _CPDeviceID; }
            set { _CPDeviceID = value; }
        }
        public int CPDongleID
        {
            get { return _CPDongleID; }
            set { _CPDongleID = value; }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public string CPDeviceDesc
        {
            get { return _CPDeviceDesc; }
            set { _CPDeviceDesc = value; }
        }
        public short MeasurementTypeID
        {
            get { return _MeasurementTypeID; }
            set { _MeasurementTypeID = value; }
        }
        public short CPDeviceTypeID
        {
            get { return _CPDeviceTypeID; }
            set { _CPDeviceTypeID = value; }
        }
        public int LastMeasurementID
        {
            get { return _LastMeasurementID; }
            set { _LastMeasurementID = value; }
        }
        #endregion
    }
}