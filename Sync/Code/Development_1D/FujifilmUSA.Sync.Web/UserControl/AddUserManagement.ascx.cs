using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Linq;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddUserManagement : BaseControl, IViewAddUserManagement
    {
        #region Declaration
        private const string _recResetPassword = "resetPasswordText";
        private const string _recActivateUserAgain = "activateUserAgainText";
        private const string _recAddUser = "lblHeaderResource1.Text";
        private const string _recEditUser = "editUserText";
        private const string _recSelectRole = "selectRoleText";
        private const string _recSelectRegion = "selectRegionText";
        private const string _recSelectCompany = "selectCompanyText";
        private const string _recEmailSent = "emailSentText";
        private const string _recprovideFirstName = "provideFirstNameText";
        private const string _recprovideLastName = "provideLastNameText";
        private const string _recprovideEmail = "provideEmailText";
        private const string _recprovideProperEmail = "provideProperEmailText";
        private const string _recprovideUsername = "provideUsernameText";
        private const string _recprovideRole = "provideRoleText";
        private const string _recprovideRegion = "provideRegionText";
        private const string _recprovideCompany = "provideCompanyText";
        private const string _recprovideBranch = "provideBranchText";

        private PresenterUserManagement _presenterUserManagement;
        public int UserId
        {
            get
            {
                return (ViewState["UserId"] != null && !string.IsNullOrEmpty(ViewState["UserId"].ToString())) ? Convert.ToInt32(ViewState["UserId"]) : 0;
            }
            set
            {
                ViewState["UserId"] = value.ToString();
            }
        }
        #endregion
        public AddUserManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterUserManagement = new PresenterUserManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (!Page.IsPostBack)
            {
                lblHeader.Text = GetResourceString(_recAddUser); //"Add User";
                BindRole();
                BindLanguages();                
                if (Request.QueryString["UserId"] != null)
                {
                    lblHeader.Text = GetResourceString(_recEditUser); //"Edit User";
                    try
                    {
                        string userID = Request.QueryString["UserId"];
                        var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(userID, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                        UserId = Convert.ToInt32(id);
                        SetUser();
                    }
                    catch
                    {
                        UserId = 0;
                    }                    
                }
                else
                {
                    trPasswordReset.Visible = false;
                }
            }
            rfvFirstNameContainer.ValidationMsg = GetResourceString(_recprovideFirstName);
            rfvLastNameContainer.ValidationMsg = GetResourceString(_recprovideLastName);
            rfvEmailContainer.ValidationMsg = GetResourceString(_recprovideEmail);
            revEmailContainer.ValidationMsg = GetResourceString(_recprovideProperEmail);
            rfvUserNameContainer.ValidationMsg = GetResourceString(_recprovideUsername);
            rfvRoleContainer.ValidationMsg = GetResourceString(_recprovideRole);
            rfvRegionContainer.ValidationMsg = GetResourceString(_recprovideRegion);
            rfvCompanyContainer.ValidationMsg = GetResourceString(_recprovideCompany);
            cvBranchContainer.ValidationMsg = GetResourceString(_recprovideBranch);
        }
        #endregion

        #region Methods
        private void BindRole()
        {
            UserRoleList = new Dictionary<int, string>();
            _presenterUserManagement.GetUserRole();
            if (!IsErrorOccured)
            {
                cmbRole.DataTextField = "Value";
                cmbRole.DataValueField = "Key";
                cmbRole.DataSource = UserRoleList;
                cmbRole.DataBind();
                cmbRole.Items.Insert(0, new ListItem(GetResourceString(_recSelectRole), ""));//--Select Role--
                cmbRole_SelectedIndexChanged(null, null);
            }
        }
        private void BindRegion()
        {
            cmbRegion.Items.Clear();
            if (string.IsNullOrEmpty(cmbRole.SelectedValue))
            {
                cmbRegion.Items.Insert(0, new ListItem(GetResourceString(_recSelectRegion), ""));//--Select Region--
            }
            else
            {
                Regions = new Dictionary<int, string>();
                _presenterUserManagement.GetRegions();
                if (!IsErrorOccured)
                {
                    cmbRegion.DataTextField = "Value";
                    cmbRegion.DataValueField = "Key";
                    cmbRegion.DataSource = Regions;
                    cmbRegion.DataBind();
                    cmbRegion.Items.Insert(0, new ListItem(GetResourceString(_recSelectRegion), ""));//--Select Region--
                }
            }
            cmbRegion_SelectedIndexChanged(null, null);
        }
        private void BindCompanies()
        {
            cmbCompany.Items.Clear();
            if (string.IsNullOrEmpty(cmbRegion.SelectedValue))
            {
                cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), ""));//--Select Company--
            }
            else
            {
                Companies = new Dictionary<int, string>();
                RegionId = Convert.ToInt32(cmbRegion.SelectedValue);
                _presenterUserManagement.GetCompaniesByRegion();
                if (!IsErrorOccured)
                {
                    cmbCompany.DataTextField = "Value";
                    cmbCompany.DataValueField = "Key";
                    cmbCompany.DataSource = Companies;
                    cmbCompany.DataBind();
                    cmbCompany.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompany), ""));//--Select Company--
                }
            }
            cmbCompany_SelectedIndexChanged(null, null);
        }
        private void BindBranch()
        {
            chkUserBranch.Items.Clear();
            if (cmbCompany.SelectedIndex != 0 && cmbCompany.SelectedValue != null)
            {
                Branches = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompany.SelectedValue);
                _presenterUserManagement.GetBranchByCompany();
                if (!IsErrorOccured)
                {
                    chkUserBranch.DataTextField = "Value";
                    chkUserBranch.DataValueField = "Key";
                    chkUserBranch.DataSource = Branches;
                    chkUserBranch.DataBind();
                }
            }
        }
        private void SaveUser()
        {
            UserInformation = new User
                                  {
                                      FirstName = txtFirstName.Text.Trim(),
                                      LastName = txtLastName.Text.Trim(),
                                      Email = txtEmail.Text.Trim(),
                                      LoginId = txtAdminUserName.Text.Trim(),
                                      IsActive = chkIsActive.Checked,
                                      LastModifiedUserId = SessionManager.Instance.CurrentUser.UserId,
                                      Role = new Role() { RoleId = Convert.ToInt16(cmbRole.SelectedValue) },
                                      UserCompany = new UserCompany(),
                                      UserBranches = new List<UserBranch>(),
                                      RegionId = 0,
                                      UserType = Convert.ToInt16(cmbUserType.SelectedItem.Value),
                                      LanguageCultureCode = ddlLanguages.SelectedValue
                                  };

            #region Set Role related information to user object
            var role = (ERole)Enum.Parse(typeof(ERole), cmbRole.SelectedItem.Text.Replace(" ", "").Trim());
            List<string> selectedBranchIds;
            switch (role)
            {
                case ERole.SystemAdmin:
                    //No need to set region/company/branch
                    break;
                case ERole.LicenseAdmin:
                    //No need to set region/company/branch
                    break;
                case ERole.WebsiteAdmin:
                    //No need to set region/company/branch
                    break;
                case ERole.RegionAdmin:
                    UserInformation.RegionId = Convert.ToInt16(cmbRegion.SelectedValue);
                    break;
                case ERole.CompanyAdmin:
                    UserInformation.UserCompany = new UserCompany() { CompanyId = Convert.ToInt16(cmbCompany.SelectedValue) };
                    break;
                case ERole.BranchAdmin:
                    UserInformation.UserCompany = new UserCompany() { CompanyId = Convert.ToInt16(cmbCompany.SelectedValue) };
                    selectedBranchIds = chkUserBranch.Items.Cast<ListItem>().ToList().FindAll(p => p.Selected).Select(x => x.Value).ToList();
                    selectedBranchIds.ForEach(x => UserInformation.UserBranches.Add(new UserBranch() { BranchId = Convert.ToInt32(x) }));
                    break;
                case ERole.SiteUser:
                    UserInformation.UserCompany = new UserCompany() { CompanyId = Convert.ToInt16(cmbCompany.SelectedValue) };
                    selectedBranchIds = chkUserBranch.Items.Cast<ListItem>().ToList().FindAll(p => p.Selected).Select(x => x.Value).ToList();
                    selectedBranchIds.ForEach(x => UserInformation.UserBranches.Add(new UserBranch() { BranchId = Convert.ToInt32(x) }));
                    break;
                case ERole.PrintBuyer:
                    UserInformation.UserCompany = new UserCompany() { CompanyId = Convert.ToInt16(cmbCompany.SelectedValue) };
                    selectedBranchIds = chkUserBranch.Items.Cast<ListItem>().ToList().FindAll(p => p.Selected).Select(x => x.Value).ToList();
                    selectedBranchIds.ForEach(x => UserInformation.UserBranches.Add(new UserBranch() { BranchId = Convert.ToInt32(x) }));
                    break;
            }
            #endregion

            if (UserId == 0) // Add New user
            {
                _presenterUserManagement.InsertUser();
                if (IsErrorOccured) return;
            }
            else // Edit New user
            {
                UserInformation.UserId = UserId;
                _presenterUserManagement.UpdateUser();
                if (IsErrorOccured) return;
            }
            Response.Redirect("UserManagement.aspx");
        }
        private void SetUser()
        {
            UserInformation = new User { UserId = UserId };
            _presenterUserManagement.GetUser();
            if (!IsErrorOccured)
            {
                txtFirstName.Text = UserInformation.FirstName;
                txtLastName.Text = UserInformation.LastName;
                txtEmail.Text = UserInformation.Email;
                txtAdminUserName.Text = UserInformation.LoginId;
                chkIsActive.Checked = UserInformation.IsActive;
                cmbRole.SelectedValue = UserInformation.Role.RoleId.ToString();
                cmbRole_SelectedIndexChanged(null, null);
                lnkPasswordReset.Text = UserInformation.IsActivated ? GetResourceString(_recResetPassword) : GetResourceString(_recActivateUserAgain);
                lnkPasswordReset.Attributes.Add("authKey", UserInformation.AuthenticationKey.ToString());
                cmbUserType.SelectedValue = UserInformation.UserType.ToString();
                if (!string.IsNullOrEmpty(UserInformation.LanguageCultureCode))
                    ddlLanguages.SelectedIndex = ddlLanguages.Items.IndexOf(ddlLanguages.Items.FindByValue(UserInformation.LanguageCultureCode));
                ManageRoleBaseUI();

                var role = (ERole)Enum.Parse(typeof(ERole), cmbRole.SelectedItem.Text.Replace(" ", "").Trim());
                switch (role)
                {
                    case ERole.SystemAdmin:
                        //nothing to set
                        break;
                    case ERole.LicenseAdmin:
                        //nothing to set
                        break;
                    case ERole.WebsiteAdmin:
                        //nothing to set
                        break;
                    case ERole.RegionAdmin:
                        if (UserInformation.RegionId > 0)
                        {
                            cmbRegion.SelectedValue = UserInformation.RegionId.ToString();
                            cmbRegion_SelectedIndexChanged(null, null);
                        }
                        break;
                    case ERole.CompanyAdmin:
                        if (UserInformation.Region != null && UserInformation.Region.RegionId > 0)
                        {
                            cmbRegion.SelectedValue = UserInformation.Region.RegionId.ToString();
                            cmbRegion_SelectedIndexChanged(null, null);
                        }
                        if (UserInformation.UserCompany != null && UserInformation.UserCompany.CompanyId > 0)
                        {
                            cmbCompany.SelectedValue = UserInformation.UserCompany.CompanyId.ToString();
                            cmbCompany_SelectedIndexChanged(null, null);
                        }
                        break;
                    case ERole.BranchAdmin:
                    case ERole.SiteUser:
                    case ERole.PrintBuyer:
                        if (UserInformation.Region != null && UserInformation.Region.RegionId > 0)
                        {
                            cmbRegion.SelectedValue = UserInformation.Region.RegionId.ToString();
                            cmbRegion_SelectedIndexChanged(null, null);
                        }
                        if (UserInformation.UserCompany != null && UserInformation.UserCompany.CompanyId > 0)
                        {
                            cmbCompany.SelectedValue = UserInformation.UserCompany.CompanyId.ToString();
                            cmbCompany_SelectedIndexChanged(null, null);
                        }
                        if (UserInformation.UserBranches != null && UserInformation.UserBranches.Count > 0)
                            chkUserBranch.Items.Cast<ListItem>().ToList().
                                ForEach(p => p.Selected = UserInformation.UserBranches.Any(u => u.BranchId.Equals(Convert.ToInt32(p.Value))));
                        break;
                }
            }
        }
        private void ManageRoleBaseUI()
        {
            trRegion.Visible = trCompany.Visible = trBranch.Visible = true;
            cmbRegion.Enabled = cmbCompany.Enabled = chkUserBranch.Enabled = true;
            if (string.IsNullOrEmpty(cmbRole.SelectedValue))
            {
                cmbRegion.Enabled = cmbCompany.Enabled = chkUserBranch.Enabled = false;
            }
            else
            {
                var role = (ERole)Enum.Parse(typeof(ERole), cmbRole.SelectedItem.Text.Replace(" ", "").Trim());
                switch (role)
                {
                    case ERole.SystemAdmin:
                        trRegion.Visible = trCompany.Visible = trBranch.Visible = false;
                        break;
                    case ERole.LicenseAdmin:
                        trRegion.Visible = trCompany.Visible = trBranch.Visible = false;
                        break;
                    case ERole.WebsiteAdmin:
                        trRegion.Visible = trCompany.Visible = trBranch.Visible = false;
                        break;
                    case ERole.RegionAdmin:
                        trCompany.Visible = trBranch.Visible = false;
                        break;
                    case ERole.CompanyAdmin:
                        trBranch.Visible = false;
                        break;
                    case ERole.BranchAdmin:
                        break;
                    case ERole.SiteUser:
                        break;
                    case ERole.PrintBuyer:
                        break;
                }
            }
        }

        private void BindLanguages()
        {
            LanguageList = new List<Language>();
            _presenterUserManagement.GetLanguages();
            if (!IsErrorOccured)
            {
                ddlLanguages.DataSource = LanguageList;
                ddlLanguages.DataTextField = "EnglishName";
                ddlLanguages.DataValueField = "CultureCode";
                ddlLanguages.DataBind();

                string defaultCultureCode = LanguageList.Where(d => d.IsDefault).Select(d => d.CultureCode).FirstOrDefault();
                if (!string.IsNullOrEmpty(defaultCultureCode))
                    ddlLanguages.SelectedIndex = ddlLanguages.Items.IndexOf(ddlLanguages.Items.FindByValue(defaultCultureCode));
                else
                    ddlLanguages.SelectedIndex = 0;
            }
        }
        #endregion

        #region Control Events
        protected void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRegion();
            ManageRoleBaseUI();
        }

        protected void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCompanies();
        }

        protected void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranch();
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SaveUser();
        }

        protected void lnkPasswordReset_Click(object sender, EventArgs e)
        {
            UserInformation = new User { UserId = UserId };
            _presenterUserManagement.GetUser();
            if (!IsErrorOccured)
            {
                UpdateType = UserInformation.IsActivated ? UserAuthenticationUpdateType.ForceReSet : UserAuthenticationUpdateType.FirstTimeSet;
                _presenterUserManagement.SendEmailToUser();
                if (!IsErrorOccured)
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recEmailSent), false);//Email has been sent to user!
                }
            }
        }
        #endregion

        #region Implement Interface
        public User UserInformation { get; set; }
        public Dictionary<int, string> Companies { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> Branches { get; set; }
        public Dictionary<int, string> Regions { get; set; }
        public Dictionary<int, string> UserRoleList { get; set; }
        public int RegionId { get; set; }
        public UserAuthenticationUpdateType UpdateType { get; set; }
        public List<Language> LanguageList { get; set; }
        #endregion
    }
}