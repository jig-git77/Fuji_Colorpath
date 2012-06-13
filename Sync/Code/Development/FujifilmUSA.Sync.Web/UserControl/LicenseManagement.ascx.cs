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
    public partial class LicenseManagement : BaseControl, IViewLicenseManagement
    {
        private PresenterLicenseManagement _presenterLicenseManagement;

        #region Declaration
        public string LicenseManagement_DeleteLicense_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("LicenseManagement_DeleteLicense_Confirmation");
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterLicenseManagement = new PresenterLicenseManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgLicense.PageSize = Common.Utilities.PageSize; 
            if (!Page.IsPostBack)
            {
                BindLicense();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //------------Apply default CurrentFilterFunction for "NumOfDevice" column of rgLicense RadGrid-----------------//
            rgLicense.Columns[1].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
            rgLicense.Columns[2].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
            rgLicense.Columns[3].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.GreaterThanOrEqualTo;
        }
        #endregion

        #region Methods

        private void GetLicense()
        {
            LicenseList = new List<License>();
            _presenterLicenseManagement.GetLicense();
        }
        private void BindLicense()
        {
            GetLicense();
            if (!IsErrorOccured)
            {
                rgLicense.DataSource = LicenseList;
                rgLicense.DataBind();
            }
        }

        protected DateTime? FillterActivationDate(GridItem item)
        {
            if (item.OwnerTableView.GetColumn("ActivationDate").CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            else
            {
                var dateValue = item.OwnerTableView.GetColumn("ActivationDate").CurrentFilterValue.Split(' ')[0];
                return DateTime.Parse(dateValue);
            }
        }
        protected DateTime? FillterExpirationDate(GridItem item)
        {
            if (item.OwnerTableView.GetColumn("ExpirationDate").CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            else
            {
                var dateValue = item.OwnerTableView.GetColumn("ExpirationDate").CurrentFilterValue.Split(' ')[0];
                return DateTime.Parse(dateValue);
            }
        }


        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddLicenseManagement.aspx");
        }
        protected void lbtnAddLicenseType_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddLicenseType.aspx");
        }
        protected void rgLicense_ItemCommand(object sender, GridCommandEventArgs e)
        {
            License = new Entity.License();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgLicense.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgLicense.MasterTableView.FilterExpression = string.Empty;
                    rgLicense.MasterTableView.Rebind();
                    break;
                case "Delete":
                    License.LicenseId = Convert.ToInt32(e.CommandArgument);
                    _presenterLicenseManagement.DeleteLicense();
                    break;
                case "Edit":
                    License.LicenseId = Convert.ToInt32(e.CommandArgument);
                    GridDataItem item = (GridDataItem)e.Item;
                    Response.Redirect("AddLicenseManagement.aspx?LicenseId=" + License.LicenseId
                                            + "&CompanyId=" + item.GetDataKeyValue("Branch.CompanyId").ToString()
                                            + "&BranchId=" + item.GetDataKeyValue("Branch.BranchId").ToString()
                                            + "&LicenseTypeId=" + item.GetDataKeyValue("LicenseTypeId").ToString()
                                            + "&LicenseDesc=" + item.GetDataKeyValue("LicenseDesc").ToString()
                                            + "&ActivationDate=" + item.GetDataKeyValue("ActivationDate").ToString()
                                            + "&ExpirationDate=" + item.GetDataKeyValue("ExpirationDate").ToString()
                                            + "&IsActive=" + item.GetDataKeyValue("IsActive").ToString()
                                            );
                    break;
            }
        }
        protected void rgLicense_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetLicense();
            if (!IsErrorOccured)
            {
                rgLicense.DataSource = LicenseList;
            }
        }


        #endregion

        #region Implement Interface
        public License License { get; set; }
        public List<License> LicenseList { get; set; }
        #endregion
    }
}