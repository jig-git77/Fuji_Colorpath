using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;


namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class OpenUpdatePopup : BaseControl, IViewUpdateChangeLog
    {
        private PresenterUpdateChangeLog _presenterUpdateChangeLog;
        private string PopupUpdateValue { get; set; }
        private string AlertPopupValue { get; set; }
        #region Constructor
        public OpenUpdatePopup()
        {
            Load += (sender, e) => ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            _presenterUpdateChangeLog = new PresenterUpdateChangeLog(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopupUpdateValue = string.Empty;
                AlertPopupValue = string.Empty;

                if (!string.IsNullOrEmpty(Request.QueryString["popupUpdateValue"]))
                {
                    PopupUpdateValue = Request.QueryString["popupUpdateValue"];
                    ViewState["PopupUpdateValue"] = PopupUpdateValue;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["alertPopupValue"]))
                {
                    AlertPopupValue = Request.QueryString["alertPopupValue"];
                    ViewState["AlertPopupValue"] = AlertPopupValue;
                }

                if (AlertPopupValue.ToLower() == "true")
                {
                    this.Page.Title = "Notice";
                    lbtnContinue.Visible = false;
                    locCancel.Text = "OK";
                    _presenterUpdateChangeLog.GetActiveUpdateChangeLogs(false, true);
                    if (resultData != null)
                    {
                        if (resultData.Tables.Count > 0)
                        {
                            if (resultData.Tables[0].Rows.Count > 0)
                            {
                                lblOpenPrintConditionMessage.Text = Convert.ToString(resultData.Tables[0].Rows[0]["Description"]);
                            }
                        }
                    }
                }
                if (PopupUpdateValue.ToLower() == "true" && AlertPopupValue.ToLower() == "false")
                {
                    lblOpenPrintConditionMessage.Text = "The ColorPath team has been hard at work to provide you with the best experience possible.";
                }
            }

        }

        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            _presenterUpdateChangeLog.UserUpdateChangeLogsStatus(SessionManager.Instance.CurrentUser.UserId, false, SessionManager.Instance.CurrentUser.IsAlertPopup);
            SessionManager.Instance.CurrentUser.IsUpdateAlert = false;
            Utilities.PopupWindowsRedirect(this, "UpdateChangeLog.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            if (ViewState["AlertPopupValue"] != null)
            {
                if (Convert.ToString(ViewState["AlertPopupValue"]).ToLower() == "true")
                {
                    _presenterUpdateChangeLog.UserUpdateChangeLogsStatus(SessionManager.Instance.CurrentUser.UserId, SessionManager.Instance.CurrentUser.IsUpdateAlert, false);
                    SessionManager.Instance.CurrentUser.IsAlertPopup = false;
                }
            }
            if (ViewState["PopupUpdateValue"] != null)
            {
                if (Convert.ToString(ViewState["PopupUpdateValue"]).ToLower() == "true")
                {
                    _presenterUpdateChangeLog.UserUpdateChangeLogsStatus(SessionManager.Instance.CurrentUser.UserId, false, SessionManager.Instance.CurrentUser.IsAlertPopup);
                    SessionManager.Instance.CurrentUser.IsUpdateAlert = false;
                }
            }
        }
        #region Implement Interface IViewUpdateChangeLog
        public List<Entity.UpdateChangeLog> UpdateChangeLogList { get; set; }
        public DataSet resultData { get; set; }
        public Entity.UpdateChangeLog objUpdateChangeLog { get; set; }
        #endregion


    }
}