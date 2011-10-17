#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
#endregion

#region Others
using Fuji.DataAccess;
#endregion


namespace Fuji.Business
{
    public abstract class CPSyncPressRun
    {
        #region Variables
        private string _TableName = "PressRun";
        private short _PressRunID = 0;
        private short _ProjectID = 0;
        private string _ProjectName = "";
        private string _PressRunName = "";
        private short _PressID = 0;
        private int _Status = 0;        
        private DateTime _StartDateTime;
        private DateTime _EndDateTime;
        private int _ΔEType = 0;
        private byte[] _ICCProfile = null;
        private short _MasterPressRunID = 0;
        private bool _UseDLP = true;   //use the DLP for previous run for the iteration
        private int _ReTargetID = 0;
        private int _ProjectTypeID = 1;
        private int _OutputTypeID;

        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public short PressRunID
        {
            get { return _PressRunID; }
            set { _PressRunID = value; }
        }
        public short ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }
        public short MasterPressRunID
        {
            get { return _MasterPressRunID; }
            set { _MasterPressRunID = value; }
        }
        public string PressRunName
        {
            get { return _PressRunName; }
            set { _PressRunName = value; }
        }
        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }
        public short PressID
        {
            get { return _PressID; }
            set { _PressID = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public DateTime StartDateTime
        {
            get { return _StartDateTime; }
            set { _StartDateTime = value; }
        }
        public DateTime EndDateTime
        {
            get { return _EndDateTime; }
            set { _EndDateTime = value; }
        }
        public int ΔEType
        {
            get { return _ΔEType; }
            set { _ΔEType = value; }
        }
        public byte[] ICCProfile
        {
            get { return _ICCProfile; }
            set { _ICCProfile = value; }
        }
        public bool UseDLP
        {
            get { return _UseDLP; }
            set { _UseDLP = value; }
        }

        public int ReTargetID
        {
            get { return _ReTargetID; }
            set { _ReTargetID = value; }
        }

        public int ProjectTypeID
        {
            get { return _ProjectTypeID; }
            set { _ProjectTypeID = value; }
        }

        public int OutputTypeID
        {
            get { return _OutputTypeID; }
            set { _OutputTypeID = value; }
        }
        #endregion
    }
}
