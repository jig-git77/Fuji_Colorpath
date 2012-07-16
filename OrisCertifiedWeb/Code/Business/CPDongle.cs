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
    public abstract class CPDongle
    {

        #region Variables
        private string _TableName = "CPDongle";
        private int _CPDongleID = 0;
        private int _BranchID = 0;
        private string _CPSerialNUM = "";
        private string _DongleDescription = "";
        private string _AllowedIPFrom = "";
        private string _AllowedIPTo = "";
        private int _LastModifiedBy = 0;
        private string _LastModifiedDate = "";
        private bool _IsUsed = true;
        #endregion

        #region Properties
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
        public string CPSerialNUM
        {
            get { return _CPSerialNUM; }
            set { _CPSerialNUM = value; }
        }
        public string DongleDescription
        {
            get { return _DongleDescription; }
            set { _DongleDescription = value; }
        }
        public string AllowedIPFrom
        {
            get { return _AllowedIPFrom; }
            set { _AllowedIPFrom = value; }
        }
        public string AllowedIPTo
        {
            get { return _AllowedIPTo; }
            set { _AllowedIPTo = value; }
        }
        public int LastModifiedBy
        {
            get { return _LastModifiedBy; }
            set { _LastModifiedBy = value; }
        }
        public string LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set { _LastModifiedDate = value; }
        }
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
        #endregion

      

    }
}