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
    public partial class AddMetricTarget : BaseControl, IViewAddMetricTarget
    {
        #region Private Variables - Globalization
        private PresenterMetricTargetManagement _presenterMetricTargetManagement;
        #endregion


        public AddMetricTarget()
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
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            //lblHeader.Text = GetResourceString(_recAddMetricTargetPreset);  //"Add MetricTargetPreset";
            //BindRegionDropDown();
            if (Request.QueryString["MetricTargetPresetId"] != null)
            {
                var MetricTargetPresetId = Request.QueryString["MetricTargetPresetId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(MetricTargetPresetId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _MetricTargetPresetId;
                if (Int32.TryParse(id, out _MetricTargetPresetId))
                {
                    hdfTargetPresetID.Value = _MetricTargetPresetId.ToString();
                    SetMetricTargetPreset(_MetricTargetPresetId);
                }
            }
        }
        #endregion

        #region Control Events
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveMetricTargetPreset();
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("MetricTargetManagement.aspx");
        }
        #endregion

        #region Methods
        private void SetMetricTargetPreset(int MetricTargetPresetId)
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset { MetricTargetPresetID = MetricTargetPresetId };
            _presenterMetricTargetManagement.GetMetricTargetPreset();
            if (!IsErrorOccured)
            {
                lblPresetName.Text = MetricTargetPreset.MetricTargetPresetName;
                grdMetricSum.DataSource = MetricTargetPreset.MetricSumTolerances;
                grdMetricSum.DataBind();
            }
        }
        private void SaveMetricTargetPreset()
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();
            MetricTargetPreset.MetricTargetPresetID = Convert.ToInt32(hdfTargetPresetID.Value);
            MetricSumList = new List<Entity.CPSyncV.MetricSum>();
            if (grdMetricSum.Rows.Count > 0)
            {
                foreach (var item in grdMetricSum.Rows)
                {
                    var row = (GridViewRow)item;
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        HiddenField hdfMetricSumId = (HiddenField)row.FindControl("hdfMetricSumId");
                        HiddenField hdfTargetMetricId = (HiddenField)row.FindControl("hdfTargetMetricId");
                        TextBox txtTargetValue = (TextBox)row.FindControl("txtTargetValue");
                        MetricSumList.Add(new Entity.CPSyncV.MetricSum()
                        {
                            MetricTargetID = Convert.ToInt32(hdfTargetMetricId.Value),
                            MetricSumId = Convert.ToInt32(hdfMetricSumId.Value),
                            TargetValue = Convert.ToDecimal(txtTargetValue.Text)
                        });
                    }
                }
            }
            _presenterMetricTargetManagement.UpdateMetricTarget();
            if (!IsErrorOccured)
            {
                Response.Redirect("~/MetricTargetManagement.aspx");
            }


        }

        #endregion

        #region Implement IViewAddMetricTargetPreset
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        //public Dictionary<int, string> Companies { get; set; }
        //public Dictionary<int, string> RegionList { get; set; }
        //public int RegionId { get; set; }
        //public List<ReferenceChartType> ReferenceChartType { get; set; }
        //public int InstrumentId { get; set; }
        //public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Entity.CPSyncV.MetricSum> MetricSumList { get; set; }
        //public int ReferenceChartTypeId { get; set; }
        //public Dictionary<int, string> ChartPatchList { get; set; }
        #endregion
    }
}