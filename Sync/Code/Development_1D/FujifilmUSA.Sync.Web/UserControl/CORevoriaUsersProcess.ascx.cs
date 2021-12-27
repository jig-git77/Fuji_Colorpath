using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class CORevoriaUsersProcess : BaseControl, IViewLogin, IViewDevice
    {
        #region "Declaration"
        private PresenterLogin _presenterlogin;
        private PresenterDevice _presenterdevice;
        #endregion


        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterlogin = new PresenterLogin(this);
            _presenterdevice = new PresenterDevice(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ProjectName,//UserId,//ProjectGuid,//DeviceId,//Media,//Colorant
                //RipSettings,//Notes,//PrintQId,//ServicePostUrl
                string StrPassword = "";                
                RevoriaUser = new RevoriaUser();
                //if (Request.QueryString["ProjectName"] != null)
                //{
                //    RevoriaUser.ProjectName = Convert.ToString(Request.QueryString["ProjectName"]).Replace("'", "");
                //}
                if (Request.QueryString["UserName"] != null)
                {
                    RevoriaUser.UserName = Convert.ToString(Request.QueryString["UserName"]).Replace("'", "");
                }
                if (Request.QueryString["Password"] != null)
                {
                    StrPassword = Convert.ToString(Request.QueryString["Password"]).Replace("'", "");
                }
                if (Request.QueryString["ProjectGuid"] != null)
                {
                    RevoriaUser.ProjectGuid = Request.QueryString["ProjectGuid"].Replace("'", "");
                }
                if (Request.QueryString["DeviceKey"] != null)
                {
                    RevoriaUser.Devicekey = Convert.ToString(Request.QueryString["DeviceKey"]).Replace("'", "");
                }
                if (Request.QueryString["Media"] != null)
                {
                    RevoriaUser.Media = Convert.ToString(Request.QueryString["Media"]).Replace("'", "");
                }
                if (Request.QueryString["Colorant"] != null)
                {
                    RevoriaUser.Colorant = Convert.ToString(Request.QueryString["Colorant"]).Replace("'", "");
                }
                if (Request.QueryString["RipSettings"] != null)
                {
                    RevoriaUser.RipSettings = Convert.ToString(Request.QueryString["RipSettings"]).Replace("'", "");
                }
                if (Request.QueryString["Notes"] != null)
                {
                    RevoriaUser.Notes = Convert.ToString(Request.QueryString["Notes"]).Replace("'", "");
                }
                if (Request.QueryString["PrintQId"] != null)
                {
                    RevoriaUser.PrintQId = Convert.ToString(Request.QueryString["PrintQId"]).Replace("'", "");
                }
                if (Request.QueryString["ServicePostUrl"] != null)
                {
                    RevoriaUser.ServicePostUrl = Convert.ToString(Request.QueryString["ServicePostUrl"]).Replace("'", "");
                }

                if (!string.IsNullOrEmpty(RevoriaUser.UserName) 
                    && !string.IsNullOrEmpty(StrPassword) 
                    //&& !string.IsNullOrEmpty(RevoriaUser.ProjectName) 
                    //&& !string.IsNullOrEmpty(RevoriaUser.ProjectGuid)
                    && !string.IsNullOrEmpty(RevoriaUser.Devicekey)
                    //&& !string.IsNullOrEmpty(RevoriaUser.Media)
                    //&& !string.IsNullOrEmpty(RevoriaUser.Colorant)
                    //&& !string.IsNullOrEmpty(RevoriaUser.RipSettings)
                    //&& !string.IsNullOrEmpty(RevoriaUser.Notes)
                    && !string.IsNullOrEmpty(RevoriaUser.PrintQId)
                    //&& !string.IsNullOrEmpty(RevoriaUser.ServicePostUrl)
                    )
                {
                    UserName = RevoriaUser.UserName;
                    Password = StrPassword;
                    _presenterlogin.UserLogin();
                    if (!IsErrorOccured)
                    {
                        if (CurrentUser != null && CurrentUser.LoginId != null)
                        {
                            RevoriaUser.ProjectName = string.Format("{0}-{1}-{2}", RevoriaUser.Devicekey, "TestRevoriaProject", RevoriaUser.PrintQId);
                            DataSet dsResult = _presenterlogin.InserTempProjectRevoriaUser(RevoriaUser);
                            SessionManager.Instance.CurrentUser = CurrentUser;
                            if (dsResult != null)
                            {
                                if (dsResult.Tables.Count > 0)
                                {
                                    if (dsResult.Tables[0].Rows.Count != 0)
                                    {
                                        DeviceId = Convert.ToInt32(dsResult.Tables[0].Rows[0]["DeviceID"].ToString());
                                        CurrentUserId = SessionManager.Instance.CurrentUser.UserId;
                                        _presenterdevice.DeviceListByDeviceId();
                                        SessionManager.Instance.CurrentDevice = SelectedDevice;
                                        SessionManager.Instance.CurrentBranchName = dsResult.Tables[0].Rows[0]["BranchName"].ToString();
                                        SessionManager.Instance.CurrentRevoriaUser = RevoriaUser;
                                        FormsAuthentication.RedirectFromLoginPage(UserName, false);
                                        Response.Redirect("RevoriaUserProjectList.aspx");
                                    }
                                }

                            }
                            
                        }
                    }
                }

            }

        }

        #endregion


        #region IViewLogin Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public User CurrentUser { get; set; }

        public int UserId { get; set; }
        public string OSName { get; set; }
        public string BrowserName { get; set; }
        public string EntryDate { get; set; }

        public string EncyptedTocken { get; set; }
        public UserReferenceChart UserReferenceChart { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        public ReferenceChartApp UserReferenceChartApp { get; set; }

        public RevoriaUser RevoriaUser { get; set; }
        #endregion


        #region IViewDevice Members
        public int TaskeroUserId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId
        {
            get
            {
                return SessionManager.Instance.SelectedCompany;
            }
            set
            {
                SessionManager.Instance.SelectedCompany = value;
            }
        }

        public int BranchId
        {
            get
            {
                return SessionManager.Instance.SelectedBranch;
            }
            set
            {
                SessionManager.Instance.SelectedBranch = value;
            }
        }

        public MeasurementDevice MeasurementDevice { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public Dictionary<int, string> BranchList { get; set; }
        public Dictionary<int, string> CompanyList { get; set; }
        public Dictionary<int, string> LicenseList { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Device> Devices { get; set; }
        public int DeviceId { get; set; }
        public Device SelectedDevice { get; set; }
        #endregion
    }
}