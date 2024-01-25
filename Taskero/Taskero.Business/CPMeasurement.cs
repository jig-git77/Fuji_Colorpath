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
    public abstract class CPMeasurement
    {

        #region Variables
        private string _TableName = "CPMeasurement";
        private string _CPMeasurementGUID = "";
        private int _CPMeasurementID = 0;
        private short _CPVClientMID = 0;
        private int _CPDongleID = 0;
        private int _CPDeviceID = 0;
        private string _Originator = "";
        private string _CreatedDT = "";
        private string _CreatedGMT = "";
        private string _User = "";
        private string _Customer = "";
        private string _Job = "";
        private string _Info_Text = "";
        private short _Status = 0;
        private short _NumOfPatches = 0;
        private short _NumOfPatchLines = 0;
        private short _CPMeasurementTypeID = 0;
        private string _TimeStamp = "";
        private bool _IsUsed = true;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public string CPMeasurementGUID
        {
            get { return _CPMeasurementGUID; }
            set { _CPMeasurementGUID = value; }
        }
        public int CPMeasurementID
        {
            get { return _CPMeasurementID; }
            set { _CPMeasurementID = value; }
        }
        public short CPVClientMID
        {
            get { return _CPVClientMID; }
            set { _CPVClientMID = value; }
        }
        public int CPDongleID
        {
            get { return _CPDongleID; }
            set { _CPDongleID = value; }
        }
        public int CPDeviceID
        {
            get { return _CPDeviceID; }
            set { _CPDeviceID = value; }
        }
        public string Originator
        {
            get { return _Originator; }
            set { _Originator = value; }
        }
        public string CreatedDT
        {
            get { return _CreatedDT; }
            set { _CreatedDT = value; }
        }
        public string CreatedGMT
        {
            get { return _CreatedGMT; }
            set { _CreatedGMT = value; }
        }
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        public string Job
        {
            get { return _Job; }
            set { _Job = value; }
        }
        public string Info_Text
        {
            get { return _Info_Text; }
            set { _Info_Text = value; }
        }
        public short Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public short NumOfPatches
        {
            get { return _NumOfPatches; }
            set { _NumOfPatches = value; }
        }
        public short NumOfPatchLines
        {
            get { return _NumOfPatchLines; }
            set { _NumOfPatchLines = value; }
        }
        public short CPMeasurementTypeID
        {
            get { return _CPMeasurementTypeID; }
            set { _CPMeasurementTypeID = value; }
        }
        public string TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
        #endregion
    }
}