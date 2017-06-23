using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfileManagement : BaseControl, IViewProfileManagement
    {
        #region Private Variables - Globalization
        private const string _recPasswordSavedSuccess = "passwordSavedSuccess";
        private const string _recSecurityInfoSavedSuccess = "securityInfoSavedSuccess";
        private const string _recSelect = "selectText";
        private const string _recChangeLanguageSuccess = "changeLanguageSuccess";
        private const string _recUserDeviceInfoSavedSuccess = "userDeviceInfoSavedSuccess";
        #endregion

        #region Variable Declarations

        private PresenterProfileManagement _presenterProfileManagement;

        #endregion

        #region Constructor
        public ProfileManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterProfileManagement = new PresenterProfileManagement(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserName.Text = SessionManager.Instance.CurrentUser.LoginId;
                GetSecurityQuestions();
                GetLanguageList();
                GetMDeviceSerialNumberList();
                GetDensityTypeList();
                GetUVMethodList();
            }
        }
        #endregion

        #region Control Events

        protected void lbtnSavePassword_Click(object sender, EventArgs e)
        {
            LoginId = SessionManager.Instance.CurrentUser.UserId;
            Password = txtNewPassword.Text;
            _presenterProfileManagement.UpdatePassword();
            if (!IsErrorOccured)
            {
                WebLogActivityManager.LogActivity(LogActivityType.ChangeProfile, null, Request.UserHostAddress);
                fujiStatusbar.DisplayMessage(GetResourceString(_recPasswordSavedSuccess), false);
            }
        }

        protected void lbtnSaveSecurity_Click(object sender, EventArgs e)
        {
            LoginId = SessionManager.Instance.CurrentUser.UserId;
            SecurityQuestionId = Convert.ToInt32(cmbSecurityQuestion.SelectedValue);
            SecurityAnswer = txtSecurityAnswer.Text.Trim();
            _presenterProfileManagement.UpdateSecurityInformation();
            if (!IsErrorOccured)
            {
                WebLogActivityManager.LogActivity(LogActivityType.ChangeProfile, null, Request.UserHostAddress);
                fujiStatusbar.DisplayMessage(GetResourceString(_recSecurityInfoSavedSuccess), false);
            }
        }

        protected void lbtnChangeLanguage_Click(object sender, EventArgs e)
        {
            LoginId = SessionManager.Instance.CurrentUser.UserId;
            LanguageCultureCode = ddlLanguages.SelectedValue;
            _presenterProfileManagement.UpdateLanguage();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentUser.LanguageCultureCode = LanguageCultureCode;
                WebLogActivityManager.LogActivity(LogActivityType.ChangeProfile, null, Request.UserHostAddress);
                fujiStatusbar.DisplayMessage(GetResourceString(_recChangeLanguageSuccess), false);
                Response.Redirect("~/ProfileManagement.aspx");
            }
        }

        protected void lbtnSaveUserDevice_Click(object sender, EventArgs e)
        {
            LoginId = SessionManager.Instance.CurrentUser.UserId;
            MeasurementDeviceID = Convert.ToInt32(cmbMeasurementDevice.SelectedValue);
            DensityType = Convert.ToInt16(cmbDensityType.SelectedValue);
            UVMode = Convert.ToInt16(cmbUVSettings.SelectedValue);
            _presenterProfileManagement.UpdateUserDeviceInfo();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentUser.MeasurementDeviceId = MeasurementDeviceID;
                SessionManager.Instance.CurrentUser.DensityType = DensityType;
                SessionManager.Instance.CurrentUser.UVMode = UVMode;
                WebLogActivityManager.LogActivity(LogActivityType.ChangeProfile, null, Request.UserHostAddress);
                fujiStatusbar.DisplayMessage(GetResourceString(_recUserDeviceInfoSavedSuccess), false);
            }
        }

        #endregion

        #region Methods
        private void GetSecurityQuestions()
        {
            _presenterProfileManagement.GetSecurityQuestionList();
            if (!IsErrorOccured)
            {
                cmbSecurityQuestion.DataSource = SecurityQuestions;
                cmbSecurityQuestion.DataBind();
                cmbSecurityQuestion.Items.Insert(0, new ListItem(GetResourceString(_recSelect), string.Empty));
                cmbSecurityQuestion.SelectedValue = SessionManager.Instance.CurrentUser.SecurityQuestionId.ToString();
                txtSecurityAnswer.Text = SessionManager.Instance.CurrentUser.SecurityAnswer.ToString();
            }
        }

        private void GetLanguageList()
        {
            LanguageList = new List<Language>();
            _presenterProfileManagement.GetLanguages();
            if (!IsErrorOccured)
            {
                ddlLanguages.DataSource = LanguageList;
                ddlLanguages.DataTextField = "EnglishName";
                ddlLanguages.DataValueField = "CultureCode";
                ddlLanguages.DataBind();
                ddlLanguages.Items.Insert(0, new ListItem(GetResourceString(_recSelect), string.Empty));
                if (!string.IsNullOrEmpty(SessionManager.Instance.CurrentUser.LanguageCultureCode))
                    ddlLanguages.SelectedIndex = ddlLanguages.Items.IndexOf(ddlLanguages.Items.FindByValue(SessionManager.Instance.CurrentUser.LanguageCultureCode));
                else
                    ddlLanguages.SelectedIndex = 0;
            }
        }

        private void GetMDeviceSerialNumberList()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            LoginId = SessionManager.Instance.CurrentUser.UserId;
            _presenterProfileManagement.GetMeasurementDevice();
          
            if (!IsErrorOccured)
            {
                cmbMeasurementDevice.DataValueField = "MeasurementDeviceId";
                cmbMeasurementDevice.DataTextField = "SerialNumber";
                cmbMeasurementDevice.DataSource = MeasurementDevices;
                cmbMeasurementDevice.DataBind();
                cmbMeasurementDevice.Items.Insert(0, new ListItem(GetResourceString(_recSelect), string.Empty));
                cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentUser.MeasurementDeviceId.ToString();
            }
        }

        private void GetDensityTypeList()
        {
            if (cmbDensityType.DataSource != null) return;

            cmbDensityType.DataValueField = "Key";
            cmbDensityType.DataTextField = "Value";
            cmbDensityType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<DensityTypes>();
            cmbDensityType.DataBind();
            cmbDensityType.SelectedValue = SessionManager.Instance.CurrentUser.DensityType.ToString();
        }

        private void GetUVMethodList()
        {
            if (cmbUVSettings.DataSource != null) return;

            cmbUVSettings.DataValueField = "Key";
            cmbUVSettings.DataTextField = "Value";
            cmbUVSettings.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EUVIncludedMode>();
            cmbUVSettings.DataBind();
            cmbUVSettings.SelectedValue = SessionManager.Instance.CurrentUser.UVMode.ToString();
        }
        #endregion

        public int LoginId { get; set; }
        public string Password { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityAnswer { get; set; }
        public Dictionary<int, string> SecurityQuestions { get; set; }
        public string LanguageCultureCode { get; set; }
        public List<Language> LanguageList { get; set; }
        public int MeasurementDeviceID { get; set; }
        public short UVMode { get; set; }
        public short DensityType { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
    }
}
