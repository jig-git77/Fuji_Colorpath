using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Login : BaseControl, IViewLogin
    {
        #region "Declaration"
        private PresenterLogin _presenterlogin;
        public string btnLoginId
        {
            get { return bntLogin.UniqueID; }
        }
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterlogin = new PresenterLogin(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (SessionManager.Instance.CurrentUser != null)
            //    Response.Redirect("Index.aspx");
            if (!IsPostBack)
            {
                if ((Request.Cookies["SyncFujiLogin"] == null || Request.Cookies["SyncFujiLogin"].Value == "") &&
                    (Request.Cookies["SyncFujiPass"] == null || Request.Cookies["SyncFujiPass"].Value == ""))
                {
                    chkRememberMe.Checked = false;
                }
                else
                {
                    chkRememberMe.Checked = true;
                    txtUserName.Text = Request.Cookies["SyncFujiLogin"].Value;
                    txtPassword.Attributes.Add("value", Request.Cookies["SyncFujiPass"].Value);

                }

                //Bind language combo
                ddlLanguages.DataSource = Languages;
                ddlLanguages.DataTextField = "Key";
                ddlLanguages.DataValueField = "Value";
                ddlLanguages.DataBind();                
            }
        }
        protected void bntLogin_Click(object sender, EventArgs e)
        {
            //FormsAuthentication.
            UserName = txtUserName.Text.Trim();
            Password = txtPassword.Text.Trim();
            _presenterlogin.UserLogin();
            if (!IsErrorOccured)
            {
                if (CurrentUser != null && CurrentUser.LoginId != null)
                {
                    SessionManager.Instance.CurrentUser = CurrentUser;

                    WebLogActivityManager.LogActivity(LogActivityType.LoggedIn, null, Request.UserHostAddress);
                    
                    SessionManager.Instance.CurrentLanguage = ddlLanguages.SelectedValue;                    
                    if (chkRememberMe.Checked == true)
                    {
                        var objCookie = new HttpCookie("SyncFujiLogin") { Value = txtUserName.Text, Expires = System.DateTime.Now.AddDays(20) };
                        Response.SetCookie(objCookie);

                        objCookie = new HttpCookie("SyncFujiPass") { Value = Password, Expires = System.DateTime.Now.AddDays(20) };
                        Response.SetCookie(objCookie);

                        objCookie = new HttpCookie("SyncFujiSelectedLanguage") { Value = ddlLanguages.SelectedValue, Expires = System.DateTime.Now.AddDays(20) };
                        Response.SetCookie(objCookie);
                    }
                    else
                    {
                        Response.Cookies["SyncFujiLogin"].Value = "";
                        Response.Cookies["SyncFujiPass"].Value = "";
                        Response.Cookies["SyncFujiSelectedLanguage"].Value = "";
                    }
                    
                    if (SessionManager.Instance.CurrentUser.HasProjectPermission())
                    {
                        FormsAuthentication.RedirectFromLoginPage(UserName, false);
                        
                        Response.Redirect("Index.aspx");
                    }
                    else if (SessionManager.Instance.CurrentUser.HasAdminPermission())
                    {
                        FormsAuthentication.RedirectFromLoginPage(UserName, false);
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
        #endregion

        #region Property
        protected Dictionary<string, string> Languages
        {
            get
            {
                Dictionary<string, string> languageList = new Dictionary<string, string>();
                languageList.Add("English", "en-US");
                languageList.Add("German", "de-DE");
                languageList.Add("Japanese", "ja-JP");
                return languageList;
            }
        }
        #endregion

        #region IViewTaskeroUser Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public User CurrentUser { get; set; }
        #endregion
    }
}
