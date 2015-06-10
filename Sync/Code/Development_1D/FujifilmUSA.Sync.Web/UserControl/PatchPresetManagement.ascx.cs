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
    public partial class PatchPresetManagement : BaseControl, IViewMetricTargetPresetManagement
    {
        private PresenterMetricTargetPresetManagement _presenterMetricTargetPresetManagement;
        public int TotalPatchPresets = 0;

         #region Declaration
        public string PatchPresetManagement_DeletePatchPreset_Confirmation
         {
             get
             {
                 return ResourceManager.GetResourceMessage("PatchPresetManagement_DeletePatchPreset_Confirmation");
             }
         }
        public string PatchPresetManagement_HitLimitPatchPresets
        {
            get
            {
                return ResourceManager.GetResourceMessage("PatchPresetManagement_HitLimitPatchPresets");
            }
        }
         #endregion

        public PatchPresetManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterMetricTargetPresetManagement = new PresenterMetricTargetPresetManagement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rgMetricTargetPreset.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                lnkbtnPatchTolerance.Visible = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.ManagePatchTolerance);
                BindMetricTargetPresets();
            }
        }
        #endregion

        #region Methods
        private void GetMetricTargetPresets()
        {
            MetricTargetPresetList = new List<Entity.CPSyncV.MetricTargetPreset>();
            _presenterMetricTargetPresetManagement.GetMetricTargetPatchPresets();

            //-----Code to check limit of 3 for Company Admin, for adding new Patch Presets-----//
            if (MetricTargetPresetList.Count > 0)
            {
                for (int i = 0; i < MetricTargetPresetList.Count; i++)
                {
                    if (MetricTargetPresetList[i].Company.CompanyId == SessionManager.Instance.CurrentUser.UserCompany.CompanyId)
                    {
                        TotalPatchPresets++;
                    }
                }
            }
        }
        private void BindMetricTargetPresets()
        {
            GetMetricTargetPresets();
            if (!IsErrorOccured)
            {
                rgMetricTargetPreset.DataSource = MetricTargetPresetList;
                rgMetricTargetPreset.DataBind();
            }
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddMetricTargetPreset.aspx");
        }
        protected void rgMetricTargetPreset_ItemCommand(object sender, GridCommandEventArgs e)
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();
            switch (e.CommandName)
            {
                case "Filter":
                    break;
                case "ClearFilter":
                    foreach (GridColumn column in rgMetricTargetPreset.MasterTableView.Columns)
                    {
                        column.CurrentFilterFunction = GridKnownFunction.NoFilter;
                        column.CurrentFilterValue = string.Empty;
                    }
                    rgMetricTargetPreset.MasterTableView.FilterExpression = string.Empty;
                    rgMetricTargetPreset.MasterTableView.Rebind();
                    break;
                case "Delete":
                    MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(e.CommandArgument);
                    _presenterMetricTargetPresetManagement.DeleteMetricTargetPreset();
                    break;
                case "Edit":
                    MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(e.CommandArgument);

                    GridDataItem item = (GridDataItem)e.Item;
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(MetricTargetPreset.MetricTargetPresetID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);

                    Response.Redirect("AddPatchTolerance.aspx?MetricTargetPresetId=" + ContentEncoder.Encode(parms));
                  
                    break;
            }
        }
        protected void rgMetricTargetPreset_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GetMetricTargetPresets();
            if (!IsErrorOccured)
            {
                rgMetricTargetPreset.DataSource = MetricTargetPresetList;
            }
        }

        protected void lnkbtnPatchTolerance_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddPatchTolerance.aspx");
        }

        #endregion

        #region Implement Interface IViewMetricTargetPresetManagement
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        #endregion

      
    }
}