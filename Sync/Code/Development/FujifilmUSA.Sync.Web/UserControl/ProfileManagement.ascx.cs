using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfileManagement : BaseControl, IViewProfileManagement
    {
        #region Variable Declarations

        private PresenterProfileManagement _presenterProfileManagement;

        #endregion

        #region Page Events
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterProfileManagement = new PresenterProfileManagement(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {              
                lblUserName.Text = SessionManager.Instance.CurrentUser.LoginId;
                GetSecurityQuestions();
            }
        }
        #endregion

        #region Control Events

        protected void lbtnSavePassword_Click(object sender, EventArgs e)
        {
            LoginId = SessionManager.Instance.CurrentUser.UserId;
            Password = txtNewPassword.Text;
            _presenterProfileManagement.UpdatePassword();
            if(!IsErrorOccured)
            {
                fujiStatusbar.DisplayMessage("Password saved successfully!", false);
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
                fujiStatusbar.DisplayMessage("Security information saved successfully!", false);
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
                cmbSecurityQuestion.Items.Insert(0, new ListItem("--Select--", string.Empty));
                cmbSecurityQuestion.SelectedValue = SessionManager.Instance.CurrentUser.SecurityQuestionId.ToString();
                txtSecurityAnswer.Text = SessionManager.Instance.CurrentUser.SecurityAnswer.ToString();
            }
        }
        #endregion

        public int LoginId { get; set; }
        public string Password { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityAnswer { get; set; }
        public Dictionary<int, string> SecurityQuestions { get; set; }
    }
}