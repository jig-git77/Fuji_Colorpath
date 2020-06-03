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
    public abstract class TaskeroUser
    {

        #region Variables
        private string _TableName = "TaskeroUser";
        private int _TaskeroUserID = 0;
        private string _LoginID = "";
        private string _LoginPass = "";
        private string _LastName = "";
        private string _FirstName = "";
        private string _Email = "";
        private bool _ShowCPVData = true;
        private int _LastModified_UserID = 0;
        private string _LastModifiedDate = "";
        private short _RoleID = 0;
        private string _Customer = "";
        private bool _IsActive = true;
        private string _CompanyID = "";
        private string _BranchID = "";
        private string _DongleID = "";
        private string _ReportID = "";
        private string _PermissionID = "";
        private string _AllowedIP = "";
        private int _BrandID = 0;
        private bool _EmailWhenYellow = true;
        private bool _EmailWhenRed = true;
        private bool _LicenseAgreed = false;
        #endregion

        #region Properties
        public string TableName
        {
            get { return _TableName; }
            set { }
        }
        public int TaskeroUserID
        {
            get { return _TaskeroUserID; }
            set { _TaskeroUserID = value; }
        }
        public string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }
        public string LoginPass
        {
            get { return _LoginPass; }
            set { _LoginPass = value; }
        }
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public bool ShowCPVData
        {
            get { return _ShowCPVData; }
            set { _ShowCPVData = value; }
        }
        public int LastModified_UserID
        {
            get { return _LastModified_UserID; }
            set { _LastModified_UserID = value; }
        }
        public string LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set { _LastModifiedDate = value; }
        }
        public short RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }
        public string BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }
        public string DongleID
        {
            get { return _DongleID; }
            set { _DongleID= value; }
        }
        public string ReportID
        {
            get { return _ReportID; }
            set { _ReportID = value; }
        }
        public string PermissionID
        {
            get { return _PermissionID; }
            set { _PermissionID = value; }
        }
        public string AllowedIP
        {
            get { return _AllowedIP; }
            set { _AllowedIP = value; }
        }
        public int BrandID { get { return _BrandID; } set { _BrandID = value; } }
        public bool EmailWhenYellow
        {
            get { return _EmailWhenYellow; }
            set { _EmailWhenYellow = value; }
        }
        public bool EmailWhenRed
        {
            get { return _EmailWhenRed; }
            set { _EmailWhenRed = value; }
        }

        public bool SiteLicenseAgreed
        {
            get { return _LicenseAgreed; }
            set { _LicenseAgreed = value; }
        }
        #endregion
    }
}