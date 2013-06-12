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
    public partial class MetricTargetPresetManagement : BaseControl, IViewMetricTargetPresetManagement
    {
        private PresenterMetricTargetPresetManagement _presenterMetricTargetPresetManagement;

        #region Declaration
        public string MetricTargetPresetManagement_DeleteMetricTargetPreset_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("MetricTargetPresetManagement_DeleteMetricTargetPreset_Confirmation");
            }
        }
        #endregion
        public MetricTargetPresetManagement()
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
                BindMetricTargetPresets();
            }
        }
        #endregion

        #region Methods
        private void GetMetricTargetPresets()
        {
            MetricTargetPresetList = new List<Entity.CPSyncV.MetricTargetPreset>();
            _presenterMetricTargetPresetManagement.GetMetricTargetPresets();
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
                    Response.Redirect("AddMetricTargetPreset.aspx?MetricTargetPresetId=" + MetricTargetPreset.MetricTargetPresetID);
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
        #endregion

        #region Implement Interface IViewMetricTargetPresetManagement
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        #endregion
    }
}