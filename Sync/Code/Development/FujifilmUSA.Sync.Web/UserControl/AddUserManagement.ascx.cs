using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Linq;

using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Collections.Generic;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddUserManagement : BaseControl, IViewAddUserManagement
    {
        private PresenterUserManagement _presenterUserManagement;

        #region Declaration
        private int UId
        {
            get
            {
                return (ViewState["UserId"] != null && !string.IsNullOrEmpty(ViewState["UserId"].ToString())) ? Convert.ToInt32(ViewState["UserId"]) : 0;
            }
            set { ViewState["UserId"] = value.ToString(); }
        }
     
        #endregion


        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterUserManagement = new PresenterUserManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            BindCompnay();
            BindRole();
            if (Request.QueryString["UserId"] != null)
            {
                SetUser(Convert.ToInt32(Request.QueryString["UserId"]));
            }
        }
        #endregion

        #region Methods

        private void BindCompnay()
        {
            Compnaies = new Dictionary<int, string>();
            _presenterUserManagement.GetCompanies();
            cmbCompanyName.DataTextField = "Value";
            cmbCompanyName.DataValueField = "Key";
            cmbCompanyName.DataSource = Compnaies;
            cmbCompanyName.DataBind();
            cmbCompanyName.Items.Insert(0, new ListItem("--Select Company--", ""));
        }
        private void BindRole()
        {
            UserRoleList = new Dictionary<int, string>();
            _presenterUserManagement.GetUserRole();
            cmbRole.DataTextField = "Value";
            cmbRole.DataValueField = "Key";
            cmbRole.DataSource = UserRoleList;
            cmbRole.DataBind();
            cmbRole.Items.Insert(0, new ListItem("--Select Role--", ""));
        }
        private void BindBranch()
        {
            chkUserBranch.Items.Clear();
            if (cmbCompanyName.SelectedIndex != 0 && cmbCompanyName.SelectedValue != null)
            {
                Branchs = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue);
                _presenterUserManagement.GetBranchByCompany();
                chkUserBranch.DataTextField = "Value";
                chkUserBranch.DataValueField = "Key";
                chkUserBranch.DataSource = Branchs;
                chkUserBranch.DataBind();
            }
        }
        private void AssingeBranch()
        {
            User = new Entity.User();
            if (UId != 0)
            {
                User.UserId = UId;
                _presenterUserManagement.GetUser();
                foreach (ListItem branch in chkUserBranch.Items)
                {
                    branch.Selected = User.UserBranches.Any(p => p.BranchId.Equals(Convert.ToInt32(branch.Value)));
                }
            }
        }
        private void BindRegion()
        {
            cmbRegion.Items.Clear();
            if (cmbCompanyName.SelectedIndex != 0 && cmbCompanyName.SelectedValue != null)
            {
                Regions = new Dictionary<int, string>();
                CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue);
                _presenterUserManagement.GetRegionByCompany();
                cmbRegion.DataTextField = "Value";
                cmbRegion.DataValueField = "Key";
                cmbRegion.DataSource = Regions;
                cmbRegion.DataBind();
            }
            else
            {
                cmbRegion.Items.Insert(0, new ListItem("--Select Region--", ""));
            }
        }

        private void SaveCompnay()
        {
            if (cmbCompanyName.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select company.", false);
                return;
            }
            if (!chkUserBranch.Items.Cast<ListItem>().ToList().Any(p => p.Selected))
            {
                ucAdminStatusbar.DisplayMessage("Please select branch.", false);
                return;
            }

            if (txtPassWord.Text.Equals(string.Empty))
            {
                ucAdminStatusbar.DisplayMessage("Please select password.", false);
                return;
            }
            if (txtConfirmPass.Text.Equals(string.Empty))
            {
                ucAdminStatusbar.DisplayMessage("Please select confirm password.", false);
                return;
            }

            if (!txtPassWord.Text.Equals(txtConfirmPass.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                ucAdminStatusbar.DisplayMessage("Password and Comfirm Password are not same.", false);
                return;
            }

            if (cmbRole.SelectedIndex == 0)
            {
                ucAdminStatusbar.DisplayMessage("Please select role.", false);
                return;
            }


            User = new User();
            User.UserCompany = new UserCompany() { CompanyId = Convert.ToInt32(cmbCompanyName.SelectedValue) };
            User.UserBranches = new List<UserBranch>();
            chkUserBranch.Items.Cast<ListItem>().ToList().FindAll(p => p.Selected).ForEach(p => User.UserBranches.Add(new UserBranch() { BranchId = Convert.ToInt32(p.Value) }));

            if (cmbRegion.SelectedIndex != 0)
                User.RegionId = Convert.ToByte(cmbRegion.SelectedValue);

            User.FirstName = txtFirstName.Text;
            User.LastName = txtLastName.Text;
            User.Email = txtEmail.Text;
            User.LoginId = txtUserName.Text;
            User.LoginPass = txtPassWord.Text;
            User.IsActive = chkIsActive.Checked;
            User.LastModifiedUserId = SessionManager.Instance.CurrentUser.UserId;
            User.Role = new Role() { RoleId = Convert.ToInt16(cmbRole.SelectedValue) };



            if (UId == 0) // Add New Compnay
            {
                _presenterUserManagement.InsertUser();
            }
            else // Edit New Compnay
            {
                User.UserId = UId;
                _presenterUserManagement.UpdateUser();
            }
            Response.Redirect("UserManagement.aspx");
        }
        private void SetUser(int userId)
        {
            User = new User {UserId = UId = userId};
            _presenterUserManagement.GetUser();

            cmbCompanyName.SelectedValue = User.UserCompany.CompanyId.ToString();

            BindBranch();
            chkUserBranch.Items.Cast<ListItem>().ToList().ForEach(p => p.Selected = User.UserBranches.Any(u => u.BranchId.Equals(Convert.ToInt32(p.Value))));

            BindRegion();

            txtFirstName.Text = User.FirstName;
            txtLastName.Text = User.LastName;
            txtEmail.Text = User.Email;
            txtUserName.Text = User.LoginId;
            txtPassWord.Text = txtConfirmPass.Text = User.LoginPass;

            txtPassWord.Attributes.Add("Value", User.LoginPass);
            txtConfirmPass.Attributes.Add("Value", User.LoginPass);

            chkIsActive.Checked = User.IsActive;
            cmbRole.SelectedValue = User.Role.RoleId.ToString();
        }
        #endregion

        #region Control Events
        protected void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            BindBranch();
            AssingeBranch();
            BindRegion();
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ucAdminStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveCompnay();
        }
        #endregion

        #region Implement Interface
        public User User { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        public int CompanyId { get; set; }
        public Dictionary<int, string> Branchs { get; set; }
        public Dictionary<int, string> Regions { get; set; }
        public Dictionary<int, string> UserRoleList { get; set; }
        #endregion
    }
}