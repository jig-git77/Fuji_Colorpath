using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMetricTargetPreset : BaseControl, IViewAddMetricTargetPreset
    {
        #region Private Variables - Globalization
        private PresenterMetricTargetPresetManagement _presenterMetricTargetPresetManagement;
        private const string _recAddMetricTargetPreset = "addMetricTargetPresetText";
        private const string _recEditMetricTargetPreset = "editMetricTargetPresetText";
        private const string _recSelectRegionText = "selectRegionText";
        private const string _recSelectCompanyText = "selectCompanyText";
        #endregion

        #region Declaration
        private int BId
        {
            get
            {
                return (ViewState["MetricTargetPresetId"] != null && !string.IsNullOrEmpty(ViewState["MetricTargetPresetId"].ToString())) ? Convert.ToInt32(ViewState["MetricTargetPresetId"]) : 0;
            }
            set { ViewState["MetricTargetPresetId"] = value.ToString(); }
        }

        public string ComingFrom
        {
            get
            {
                return (Request.QueryString["ComingFrom"] != null ? Request.QueryString["ComingFrom"].ToString() : "");
            }
        }
        #endregion

        public AddMetricTargetPreset()
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
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recAddMetricTargetPreset);  //"Add MetricTargetPreset";
            BindRegionDropDown();
            if (Request.QueryString["MetricTargetPresetId"] != null)
            {
                var MetricTargetPresetId = Request.QueryString["MetricTargetPresetId"];
                var id = ContentEncoder.Decode(Service.Common.Encryption.CryptHelper.DecryptString(MetricTargetPresetId, FujifilmUSA.Sync.Business.ApplicationConfiguration.Instance.EncryptionKey));
                int _MetricTargetPresetId;
                if (Int32.TryParse(id, out _MetricTargetPresetId))
                {
                    SetMetricTargetPreset(_MetricTargetPresetId);
                    lblHeader.Text = GetResourceString(_recEditMetricTargetPreset); //"Edit MetricTargetPreset"; 
                }
            }
        }
        #endregion

        #region Control Events
        protected void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bindcompany();
        }       

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            SaveMetricTargetPreset();
        }
        #endregion

        #region Methods
        private void BindRegionDropDown()
        {
            _presenterMetricTargetPresetManagement.RegionList();
            if (!IsErrorOccured)
            {
                cmbRegion.DataSource = RegionList;
                cmbRegion.DataBind();
                cmbRegion.Items.Insert(0, new ListItem(GetResourceString(_recSelectRegionText), "0"));
            }
        }

        private void Bindcompany()
        {
            Companies = new Dictionary<int, string>();
            RegionId = Convert.ToInt32(cmbRegion.SelectedValue);
            _presenterMetricTargetPresetManagement.GetCompaniesByRegion();
            if (!IsErrorOccured)
            {
                cmbCompanyName.DataTextField = "Value";
                cmbCompanyName.DataValueField = "Key";
                cmbCompanyName.DataSource = Companies;
                cmbCompanyName.DataBind();
                cmbCompanyName.Items.Insert(0, new ListItem(GetResourceString(_recSelectCompanyText), "0"));
            }
        }

        private void SetMetricTargetPreset(int MetricTargetPresetId)
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset { MetricTargetPresetID = MetricTargetPresetId };
            _presenterMetricTargetPresetManagement.GetMetricTargetPreset();
            if (!IsErrorOccured)
            {
                BId = MetricTargetPresetId;
                txtPresetName.Text = MetricTargetPreset.MetricTargetPresetName;
                txtDescription.Text = MetricTargetPreset.MetricTargetPresetDesc;
                if (MetricTargetPreset.Region.RegionId > 0)
                {
                    cmbRegion.SelectedValue = Convert.ToString(MetricTargetPreset.Region.RegionId);
                }
                cmbRegion_SelectedIndexChanged(null, null);
                if (MetricTargetPreset.Company.CompanyId > 0)
                {
                    cmbCompanyName.SelectedValue = MetricTargetPreset.Company.CompanyId.ToString();
                }
                cmbEType.SelectedValue = MetricTargetPreset.dEMethod;
                cmbValidationType.SelectedValue = MetricTargetPreset.ValidationType.ToString();
                chklist_PresetType.Items[0].Selected = MetricTargetPreset.IsSync1D;
                chklist_PresetType.Items[1].Selected = MetricTargetPreset.Validation_1D;
                chklist_PresetType.Items[2].Selected = MetricTargetPreset.Validation_4D;
                chkIsActive.Checked = MetricTargetPreset.IsUsed;             
            }
        }

        private void SaveMetricTargetPreset()
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();

            if (Convert.ToInt32(cmbRegion.SelectedValue) > 0)
            {
                MetricTargetPreset.RegionId = Convert.ToByte(cmbRegion.Text);
            }

            if (cmbCompanyName.Items.Count > 0)
            {
                if (Convert.ToInt32(cmbCompanyName.SelectedValue) > 0)
                {
                    MetricTargetPreset.CompanyId = Convert.ToInt32(cmbCompanyName.Text);

                }
            }
            MetricTargetPreset.MetricTargetPresetName = txtPresetName.Text;
            MetricTargetPreset.MetricTargetPresetDesc = txtDescription.Text;
            MetricTargetPreset.dEMethod = cmbEType.Text;
            MetricTargetPreset.ValidationType = Convert.ToInt16(cmbValidationType.SelectedValue);
            MetricTargetPreset.IsSync1D = chklist_PresetType.Items[0].Selected;
            MetricTargetPreset.Validation_1D = chklist_PresetType.Items[1].Selected;
            MetricTargetPreset.Validation_4D = chklist_PresetType.Items[2].Selected;
            MetricTargetPreset.IsUsed = chkIsActive.Checked;

            if (BId == 0) // Add New MetricTargetPreset
            {
                _presenterMetricTargetPresetManagement.InsertMetricTargetPreset();
                if (IsErrorOccured) return;
            }
            else // Edit New MetricTargetPreset
            {
                MetricTargetPreset.MetricTargetPresetID = BId;
                _presenterMetricTargetPresetManagement.UpdateMetricTargetPreset();
                if (IsErrorOccured) return;
            }
           
            if (ComingFrom == "PTP")
            {
                Response.Redirect("PatchPresetManagement.aspx");
            }
            else
            {
                Response.Redirect("MetricTargetPresetManagement.aspx");
            }
        }
        #endregion



        #region Implement IViewAddMetricTargetPreset
        public Entity.CPSyncV.MetricTargetPreset MetricTargetPreset { get; set; }
        public Dictionary<int, string> Companies { get; set; }
        public Dictionary<int, string> RegionList { get; set; }
        public int RegionId { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        public int InstrumentId { get; set; }
        public Dictionary<int, string> InstrumentTypeList { get; set; }
        public List<Entity.CPSyncV.MetricSum> MetricSumList { get; set; }
        public int ReferenceChartTypeId { get; set; }
        public Dictionary<int, string> ChartPatchList { get; set; }
        #endregion

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            if (ComingFrom == "PTP")
            {
                Response.Redirect("PatchPresetManagement.aspx");
            }
            else
            {
                Response.Redirect("MetricTargetPresetManagement.aspx");
            }
        }
    }
}