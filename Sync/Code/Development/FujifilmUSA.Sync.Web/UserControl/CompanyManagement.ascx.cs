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

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterCompanyManagement = new PresenterCompanyManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
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
            rgCompany.DataSource = CompanyList;
            rgCompany.DataBind();
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
                case "Delete" :
                    Company.CompanyId = Convert.ToInt32(e.CommandArgument);   
                    _presenterCompanyManagement.DeleteCompany();
                    break;
                case "Edit":
                    Company.CompanyId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item; 
                    int RegionId =  Convert.ToInt32(item.GetDataKeyValue("RegionId").ToString());   
                    Response.Redirect("AddCompanyManagement.aspx?CompanyId=" + Company.CompanyId 
                                            + "&CompanyName=" +  item.GetDataKeyValue("CompanyName").ToString()
                                            + "&RegionId=" + item.GetDataKeyValue("RegionId").ToString()
                                            + "&IsActive=" + item.GetDataKeyValue("IsActive").ToString()
                                            );  
                    break;
            }
        }
        protected void rgCompany_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetCompanies();
            rgCompany.DataSource = CompanyList;
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