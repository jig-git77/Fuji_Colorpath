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
    public partial class CompanyManagement : BaseControl, IViewCompanyManagement
    {
        private PresenterCompanyManagement _presenterCompanyManagement;
        #region Declaration
        public string CompanyManagement_DeleteCompany_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("CompanyManagement_DeleteCompany_Confirmation");
            }
        }
        #endregion
        public CompanyManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterCompanyManagement = new PresenterCompanyManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgCompany.PageSize = Common.Utilities.PageSize;
        }
        #endregion

        #region Methods
        public void GetCompanies()
        {
            CompanyList = new List<Entity.Company>();
            _presenterCompanyManagement.GetCompanies();
        }
        public void BindCompanies()
        {
            if (!IsErrorOccured)
            {
                rgCompany.DataSource = CompanyList;
                rgCompany.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void rgCompany_ItemCommand(object sender, GridCommandEventArgs e)
        {
            Company = new Entity.Company();
            switch (e.CommandName)
            {
                case "Fillter":
                    foreach (GridColumn column in rgCompany.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgCompany.MasterTableView.FilterExpression = string.Empty;
                    rgCompany.MasterTableView.Rebind();
                    break;
                case "Delete":
                    Company.CompanyId = Convert.ToInt32(e.CommandArgument);
                    _presenterCompanyManagement.DeleteCompany();
                    break;
                case "Edit":
                    Company.CompanyId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    int RegionId = Convert.ToInt32(item.GetDataKeyValue("RegionId").ToString());
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(Company.CompanyId.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddCompanyManagement.aspx?CompanyId=" + ContentEncoder.Encode(parms));
                    break;
            }
        }
        protected void rgCompany_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetCompanies();
            if (!IsErrorOccured)
            {
                rgCompany.DataSource = CompanyList;
            }
        }
        protected void rgCompany_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }
       
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddCompanyManagement.aspx");
        }
        #endregion

        #region Implement Interface
        public Company Company { get; set; }
        public List<Company> CompanyList { get; set; }
        #endregion
    }
}