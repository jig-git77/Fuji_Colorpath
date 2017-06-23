using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using System.Web.Security;

namespace FujifilmUSA.Sync.Web.UserControl
{
    [Serializable]
    public class UserAuthenticationInformation
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string LoginPass { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Guid AuthenticationKey { get; set; }
        public bool IsActivated { get; set; }
        public int SecurityQuestionId { get; set; }
        public string SecurityAnswer { get; set; }
    }

    public partial class GlobalActivation : BaseControl, IViewGlobalActivation
    {
        #region Variable Declarations

        private const string VsUserInformation = "_vsUserInformation";
        private PresenterGlobalActivation _presenterGlobalActivation;
        private const string _recprovideSecurityQue = "provideSecurityQue";
        private const string _recprovideSecurityAns = "provideSecurityAns";
        private const string _recincorrectURL = "incorrectURL";
        private const string _recinvalidSecurityAns = "invalidSecurityAns";
        private const string _recselectText = "selectText";
        private const string _recuserAlreadyActivated = "userAlreadyActivated";
        private const string _recinvalidKeyPassed = "invalidKeyPassed";
        #endregion

        #region Constructor
        public GlobalActivation()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterGlobalActivation = new PresenterGlobalActivation(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["uid"] != null)
                {
                    Guid uid;
                    if (!Guid.TryParse(Request.QueryString["uid"], out uid))
                    {
                        fujiStatusbar.DisplayMessage(GetResourceString(_recincorrectURL), false);
                    }
                    else
                    {
                        GetSecurityQuestions();
                        GetUserInformation(uid);
                    }
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recincorrectURL), false);
                }
            }
            rfvSecurityAnswerContainer.ValidationMsg = GetResourceString(_recprovideSecurityAns);
            rfvSecurityQuestionContainer.ValidationMsg = GetResourceString(_recprovideSecurityQue);
        }
        #endregion

        #region Control Events

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            var authUser = (UserAuthenticationInformation)ViewState[VsUserInformation];
            User = new User()
                       {
                           UserId = authUser.UserId,
                           LoginId = authUser.LoginId,
                           LoginPass = authUser.LoginPass,
                           LastName = authUser.LastName,
                           FirstName = authUser.FirstName,
                           Email = authUser.Email,
                           AuthenticationKey = authUser.AuthenticationKey,
                           IsActivated = authUser.IsActivated,
                           SecurityQuestionId = authUser.SecurityQuestionId,
                           SecurityAnswer = authUser.SecurityAnswer
                       };
            if (Request.QueryString["type"] != null && Request.QueryString["type"].StartsWith("r"))
            {
                UpdateType = UserAuthenticationUpdateType.Reset;
                if (User.SecurityAnswer.Equals(txtSecurityAnswer.Text.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    User.LoginPass = txtAdminPassWord.Text;
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recinvalidSecurityAns), false);
                    return;
                }
            }
            else if (Request.QueryString["type"] != null && Request.QueryString["type"].StartsWith("fr"))
            {
                UpdateType = UserAuthenticationUpdateType.ForceReSet;
                User.LoginPass = txtAdminPassWord.Text;
            }
            else
            {
                UpdateType = UserAuthenticationUpdateType.FirstTimeSet;
                User.SecurityQuestionId = Convert.ToInt32(cmbSecurityQuestion.SelectedValue);
                User.SecurityAnswer = txtSecurityAnswer.Text.Trim();
                User.LoginPass = txtAdminPassWord.Text;
            }
            var result = _presenterGlobalActivation.UpdateUserAuthenticationInformation();
            if (result > 0)
            {
                _presenterGlobalActivation.GetUserType();
                if (User.UserType == (int)UserType.XmfColorOrganizerUser)
                {
                    fujiStatusbar.DisplayMessage(ResourceManager.GetResourceMessage("Login_PasswordSaved"), true);
                    tbPasswordForm.Visible = false;
                }
                else
                {
                    _presenterGlobalActivation.UserLogin();
                    if (!IsErrorOccured)
                    {
                        if (User.LoginId != null)
                        {
                            SessionManager.Instance.CurrentUser = User;

                            if (SessionManager.Instance.CurrentUser.HasProjectPermission())
                            {
                                FormsAuthentication.RedirectFromLoginPage(User.LoginId, false);
                                Response.Redirect("Index.aspx");
                            }
                            else if (SessionManager.Instance.CurrentUser.HasAdminPermission())
                            {
                                FormsAuthentication.RedirectFromLoginPage(User.LoginId, false);
                                Response.Redirect("Settings.aspx");
                            }
                            else
                            {
                                fujiStatusbar.DisplayMessage(ResourceManager.GetResourceMessage("Error_NoPermission"), false);
                            }
                        }
                        else
                        {
                            fujiStatusbar.DisplayMessage(ResourceManager.GetResourceMessage("Login_InvalidLogin"), false);
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void GetSecurityQuestions()
        {
            _presenterGlobalActivation.GetSecurityQuestionList();
            if (!IsErrorOccured)
            {
                cmbSecurityQuestion.DataSource = SecurityQuestions;
                cmbSecurityQuestion.DataBind();
                cmbSecurityQuestion.Items.Insert(0, new ListItem(GetResourceString(_recselectText), string.Empty));
            }
        }

        private void GetUserInformation(Guid authenticationKey)
        {
            User = new User() { AuthenticationKey = authenticationKey };
            _presenterGlobalActivation.GetUserAuthenticationInformation();
            if (User.UserId > 0)
            {
                lblFirstName.Text = User.FirstName;
                lblLastName.Text = User.LastName;

                if (Request.QueryString["type"] != null && Request.QueryString["type"].StartsWith("r"))
                {
                    cmbSecurityQuestion.SelectedValue = User.SecurityQuestionId.ToString();
                    cmbSecurityQuestion.Enabled = false;
                    txtSecurityAnswer.Text = string.Empty;
                    txtAdminPassWord.Text = string.Empty;
                    txtConfirmPass.Text = string.Empty;
                }
                else if (Request.QueryString["type"] != null && Request.QueryString["type"].StartsWith("fr"))
                {
                    trSecurityQuestion.Visible = false;
                    trSecurityAnswer.Visible = false;
                    txtAdminPassWord.Text = string.Empty;
                    txtConfirmPass.Text = string.Empty;
                }
                else
                {
                    if (User.IsActivated)
                    {                        
                        fujiStatusbar.DisplayMessage(GetResourceString(_recuserAlreadyActivated), false);
                        lbtnSave.Enabled = false;
                        return;
                    }
                    txtSecurityAnswer.Text = string.Empty;
                    txtAdminPassWord.Text = string.Empty;
                    txtConfirmPass.Text = string.Empty;
                }
                var authUser = new UserAuthenticationInformation()
                                   {
                                       UserId = User.UserId,
                                       LoginId = User.LoginId,
                                       LoginPass = User.LoginPass,
                                       LastName = User.LastName,
                                       FirstName = User.FirstName,
                                       Email = User.Email,
                                       AuthenticationKey = User.AuthenticationKey,
                                       IsActivated = User.IsActivated,
                                       SecurityQuestionId = User.SecurityQuestionId,
                                       SecurityAnswer = User.SecurityAnswer
                                   };
                ViewState[VsUserInformation] = authUser;
            }
            else
            {                
                fujiStatusbar.DisplayMessage(GetResourceString(_recuserAlreadyActivated), false);
            }
        }

        #endregion

        #region IViewGlobalActivation Members

        public User User { get; set; }

        public Dictionary<int, string> SecurityQuestions { get; set; }

        public UserAuthenticationUpdateType UpdateType { get; set; }

        #endregion
    }
}