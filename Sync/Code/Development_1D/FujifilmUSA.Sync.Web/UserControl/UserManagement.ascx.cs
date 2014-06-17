using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class UserManagement : BaseControl, IViewUserManagement
    {
        private PresenterUserManagement _presenterUserManagement;

        #region Declaration
        public string UserManagement_DeleteUser_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("UserManagement_DeleteUser_Confirmation");
            }
        }
        #endregion

        #region Constructor
        public UserManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterUserManagement = new PresenterUserManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgUser.PageSize = Common.Utilities.PageSize; 
            if (!Page.IsPostBack)
            {
                BindUsers();
            }
        }
        #endregion

        #region Methods
        private void GetUsers()
        {
            UserList = new List<User>();
            _presenterUserManagement.GetUsers();
        }
        private void BindUsers()
        {
            GetUsers();
            if (!IsErrorOccured)
            {
                rgUser.DataSource = UserList;
                rgUser.DataBind();
            }
        }

        
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddUserManagement.aspx");
        }
        protected void rgUser_ItemCommand(object sender, GridCommandEventArgs e)
        {
            User = new Entity.User();
            switch (e.CommandName)
            {
                case "Filter" :
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgUser.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgUser.MasterTableView.FilterExpression = string.Empty;
                    rgUser.MasterTableView.Rebind();
                    break;
                case "Delete" :
                    User.UserId = Convert.ToInt32(e.CommandArgument);
                    _presenterUserManagement.DeleteUser();
                    break;
                case "Edit":
                    User.UserId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(User.UserId.ToString(), System.Configuration.ConfigurationManager.AppSettings["CypherKey"]);
                    Response.Redirect("AddUserManagement.aspx?UserId="  + ContentEncoder.Encode(parms));                                          
                    break;
            }
        }
        protected void rgUser_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetUsers();
            if (!IsErrorOccured)
            {
                rgUser.DataSource = UserList;
            }
        }
      

         #endregion

        #region Implement Interface
        public User User { get; set; }
        public List<User> UserList { get; set; }
        public Dictionary<int, string> Compnaies { get; set; }
        #endregion
    }
}