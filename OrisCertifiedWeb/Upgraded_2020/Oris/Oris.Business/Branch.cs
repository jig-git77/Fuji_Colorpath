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
    public abstract class Branch
    {
        #region Variables
        private string _TableName = "Branch";
        private int _BranchID = 0;
        private int _CompanyID = 0;
        private string _BranchName = "";
        private string _BranchLocation = "";
        private string _PrimaryContactName = "";
        private string _Tel_CountryCode = "";
        private string _Tel_PhoneNo = "";
        private string _Tel_Extension = "";
        private string _PrimaryContactEmail = "";
        private string _PrimaryContactCell = "";
        private int _LastModifiedBy = 0;
        private string _LastModifiedDate = "";
        private bool _IsActive = true;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public int CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        public string BranchLocation
        {
            get { return _BranchLocation; }
            set { _BranchLocation = value; }
        }
        public string PrimaryContactName
        {
            get { return _PrimaryContactName; }
            set { _PrimaryContactName = value; }
        }
        public string Tel_CountryCode
        {
            get { return _Tel_CountryCode; }
            set { _Tel_CountryCode = value; }
        }
        public string Tel_PhoneNo
        {
            get { return _Tel_PhoneNo; }
            set { _Tel_PhoneNo = value; }
        }
        public string Tel_Extension
        {
            get { return _Tel_Extension; }
            set { _Tel_Extension = value; }
        }
        public string PrimaryContactEmail
        {
            get { return _PrimaryContactEmail; }
            set { _PrimaryContactEmail = value; }
        }
        public string PrimaryContactCell
        {
            get { return _PrimaryContactCell; }
            set { _PrimaryContactCell = value; }
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
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        #endregion    
    }
}