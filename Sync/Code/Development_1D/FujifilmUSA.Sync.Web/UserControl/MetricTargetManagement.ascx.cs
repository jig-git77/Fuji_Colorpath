using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MetricTargetManagement : BaseControl, IViewMetricTargetManagement
    {
        private PresenterMetricTargetManagement _presenterMetricTargetManagement;

        public MetricTargetManagement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterMetricTargetManagement = new PresenterMetricTargetManagement(this);            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //rgMetricTargetPreset.PageSize = Common.Utilities.PageSize;
            if (!Page.IsPostBack)
            {
                BindMetricTargets();                
            }
        }
        #endregion

        #region Control Events
        protected void likAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("AddReferenceChartPreset.aspx");
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
                case "Edit":
                    MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(e.CommandArgument);

                    GridDataItem item = (GridDataItem)e.Item;
                    string parms = Service.Common.Encryption.CryptHelper.EncryptString(MetricTargetPreset.MetricTargetPresetID.ToString(), FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey);
                    Response.Redirect("AddMetricTarget.aspx?MetricTargetPresetId=" + ContentEncoder.Encode(parms));                    
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
        protected void rgMetricTargetPreset_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            MetricTargetPresetID = Convert.ToInt32(dataItem.GetDataKeyValue("MetricTargetPresetID").ToString());
            _presenterMetricTargetManagement.GetMetricTargetSumList();
            if (!IsErrorOccured)
            {
                //SessionManager.Instance.ProjectList = Projects;
                e.DetailTableView.DataSource = MetricSumTolerancesList;
            }
        }
        #endregion

        #region Methods   
        private void GetMetricTargetPresets()
        {
            MetricTargetPresetList = new List<Entity.CPSyncV.MetricTargetPreset>();
            _presenterMetricTargetManagement.GetMetricTargetPresets();
        }
        private void BindMetricTargets()
        {
            GetMetricTargetPresets();
            if (!IsErrorOccured)
            {
                rgMetricTargetPreset.DataSource = MetricTargetPresetList;
                rgMetricTargetPreset.DataBind();
            }
        }
        #endregion

        #region Implement Interface IViewMetricTargetManagement
        public int MetricTargetPresetID { get; set; }
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPresetList { get; set; }
        public List<Entity.CPSyncV.MetricSum> MetricSumTolerancesList { get; set; }
        #endregion 
    }
}