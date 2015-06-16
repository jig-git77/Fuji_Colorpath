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
    public partial class AddPatchTolerance : BaseControl, IViewAddMetricTargetPreset
    {
        #region Private Variables - Globalization
        private PresenterMetricTargetPresetManagement _presenterMetricTargetPresetManagement;
        private const string _recAddMetricTargetPreset = "addMetricTargetPresetText";
        private const string _recEditMetricTargetPreset = "editMetricTargetPresetText";
        private const string _recSelectRegionText = "selectRegionText";
        private const string _recSelectCompanyText = "selectCompanyText";
        private const string _recSelectText = "selectText";
        private const string _recSelectInstrumentType = "selectInstrumentTypeText";
        private const string _recErrorSamePatchTol = "errorSamePatchTol";
        private const string _recErrorTolMissing = "errorTolMissing";

        private const string _recprovidePresetName = "providePresetNameText";
        private const string _recprovideAlignmentChart = "provideAlignmentChartText";
        private const string _recprovideInstrumentType = "provideInstrumentTypeText";

        #endregion

        #region Declaration
        public string AddPatchPreset_JavascriptTollimit1
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddPatchPreset_JavascriptTollimit1");
            }
        }
        public string AddPatchPreset_JavascriptTollimit2
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddPatchPreset_JavascriptTollimit2");
            }
        }
        private int BId
        {
            get
            {
                return (ViewState["MetricTargetPresetId"] != null && !string.IsNullOrEmpty(ViewState["MetricTargetPresetId"].ToString())) ? Convert.ToInt32(ViewState["MetricTargetPresetId"]) : 0;
            }
            set { ViewState["MetricTargetPresetId"] = value.ToString(); }
        }
        #endregion

        public AddPatchTolerance()
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

            rfvPresetContainer.ValidationMsg = GetResourceString(_recprovidePresetName);
            rfvcAlignmentChartContainer.ValidationMsg = GetResourceString(_recprovideAlignmentChart);
            rfvcInstrumentTypeContainer.ValidationMsg = GetResourceString(_recprovideInstrumentType);

            if (Page.IsPostBack) return;

            lblHeader.Text = GetResourceString(_recAddMetricTargetPreset);  //"Add MetricTargetPreset";
            BindRegionDropDown();
            cmbRegion_SelectedIndexChanged(null, null);
            BindInstrumentTypeDropDown();
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
            if (SessionManager.Instance.CurrentUser.Role.RoleId == 4 && cmbCompanyName.SelectedIndex == 0)  //For Region Admin, needs to select Region and Company Name first
            {
                fujiStatusbar.DisplayMessage("Please select Company first.", false);
                return;
            }
            SaveMetricTargetPreset();
        }

        protected void lvMTPTolerances_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //----Just use this event to remove NULL tolerances display in textboxes when we edit MetricTargetPatchPreset----//
            if (e.Item is ListViewDataItem)
            {
                ListViewDataItem item = (ListViewDataItem)e.Item;

                TextBox txtTolerance = (TextBox)e.Item.FindControl("txtTolerance");
                if (txtTolerance.Text == "-1")
                {
                    txtTolerance.Text = "";
                }
            }
        }

        protected void cmbInstrumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInstrumentType.SelectedIndex != 0)
            {
                InstrumentId = Convert.ToInt32(cmbInstrumentType.SelectedValue);
                BindValidationChart();
            }
        }

        protected void cmbAlignmentChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlignmentChart.SelectedIndex > 0)
            {
                TR_MetricTolerance.Visible = true;
                BindTolerances();
                BindChartPatchList();
            }
            else
            {
                TR_MetricTolerance.Visible = false;
            }
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
            if (cmbRegion.Items.Count == 2) //---By default give selected region for Region Admin
            {
                cmbRegion.SelectedIndex = 1;
                cmbRegion.Enabled = false;
            }
        }

        private void Bindcompany()
        {
            cmbCompanyName.Enabled = true;
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
            if (cmbCompanyName.Items.Count == 2) //---By default give selected company for Company Admin
            {
                cmbCompanyName.SelectedIndex = 1;
                cmbCompanyName.Enabled = false;
            }
        }

        private void BindInstrumentTypeDropDown()
        {
            _presenterMetricTargetPresetManagement.InstrumentTypeList();
            if (!IsErrorOccured)
            {
                cmbInstrumentType.DataSource = InstrumentTypeList;
                cmbInstrumentType.DataBind();
                cmbInstrumentType.Items.Insert(0, new ListItem(GetResourceString(_recSelectInstrumentType), ""));//-- Select Instrument Type --
            }
        }

        private void BindValidationChart()
        {
            _presenterMetricTargetPresetManagement.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
                cmbAlignmentChart.DataTextField = "ChartName";
                cmbAlignmentChart.DataSource = ReferenceChartType;
                cmbAlignmentChart.DataBind();
                cmbAlignmentChart.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
            }
        }

        private void BindTolerances()
        {
            _presenterMetricTargetPresetManagement.GetMetricSumList();
            if (!IsErrorOccured)
            {
                lvMTPTolerances.DataSource = MetricSumList;
                lvMTPTolerances.DataBind();
            }
        }

        private void BindChartPatchList()
        {
            ReferenceChartTypeId = Convert.ToInt32(cmbAlignmentChart.SelectedValue);
            txtPatchTolerance1.Text = "";
            txtPatchTolerance2.Text = "";
            txtPatchTolerance3.Text = "";
            txtPatchTolerance4.Text = "";
            txtPatchTolerance5.Text = "";

            _presenterMetricTargetPresetManagement.BindChartPatchList();
            if (!IsErrorOccured)
            {
                cmbPatchSampleList1.DataSource = ChartPatchList;
                cmbPatchSampleList1.DataBind();
                cmbPatchSampleList1.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
                cmbPatchSampleList2.DataSource = ChartPatchList;
                cmbPatchSampleList2.DataBind();
                cmbPatchSampleList2.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
                cmbPatchSampleList3.DataSource = ChartPatchList;
                cmbPatchSampleList3.DataBind();
                cmbPatchSampleList3.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
                cmbPatchSampleList4.DataSource = ChartPatchList;
                cmbPatchSampleList4.DataBind();
                cmbPatchSampleList4.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
                cmbPatchSampleList5.DataSource = ChartPatchList;
                cmbPatchSampleList5.DataBind();
                cmbPatchSampleList5.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
            }
        }

        private void SetMetricTargetPreset(int MetricTargetPresetId)
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset { MetricTargetPresetID = MetricTargetPresetId };
            _presenterMetricTargetPresetManagement.GetMetricTargetPatchPreset();
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
             
                chkIsActive.Checked = MetricTargetPreset.IsUsed;
                if (MetricTargetPreset.InstrumentTypeID > 0)
                {
                    cmbInstrumentType.SelectedValue = Convert.ToString(MetricTargetPreset.InstrumentTypeID);
                }
                cmbInstrumentType_SelectedIndexChanged(null, null);
                if (MetricTargetPreset.ReferenceChartTypeId > 0)
                {
                    cmbAlignmentChart.SelectedValue = Convert.ToString(MetricTargetPreset.ReferenceChartTypeId);
                }
                cmbAlignmentChart_SelectedIndexChanged(null, null);

                if (MetricTargetPreset.MetricSumTolerances.Count > 0)
                {
                    lvMTPTolerances.DataSource = MetricTargetPreset.MetricSumTolerances;
                    lvMTPTolerances.DataBind();
                }
                if (MetricTargetPreset.PatchTolerances.Count > 0)
                {
                    cmbPatchSampleList1.Items.FindByText(MetricTargetPreset.PatchTolerances[0].SampleName).Selected = true;
                    txtPatchTolerance1.Text = MetricTargetPreset.PatchTolerances[0].Tolerance.ToString();

                    cmbPatchSampleList2.Items.FindByText(MetricTargetPreset.PatchTolerances[1].SampleName).Selected = true;
                    txtPatchTolerance2.Text = MetricTargetPreset.PatchTolerances[1].Tolerance.ToString();

                    cmbPatchSampleList3.Items.FindByText(MetricTargetPreset.PatchTolerances[2].SampleName).Selected = true;
                    txtPatchTolerance3.Text = MetricTargetPreset.PatchTolerances[2].Tolerance.ToString();

                    cmbPatchSampleList4.Items.FindByText(MetricTargetPreset.PatchTolerances[3].SampleName).Selected = true;
                    txtPatchTolerance4.Text = MetricTargetPreset.PatchTolerances[3].Tolerance.ToString();

                    cmbPatchSampleList5.Items.FindByText(MetricTargetPreset.PatchTolerances[4].SampleName).Selected = true;
                    txtPatchTolerance5.Text = MetricTargetPreset.PatchTolerances[4].Tolerance.ToString();
                }
            }
        }

        private void SaveMetricTargetPreset()
        {
            MetricTargetPreset = new Entity.CPSyncV.MetricTargetPreset();

            //---Items to add into CPSyncV.MetricTargetPreset table----//
            if (Convert.ToInt32(cmbRegion.SelectedValue) > 0)
            {
                MetricTargetPreset.RegionId = Convert.ToByte(cmbRegion.Text);
            }

            if (cmbCompanyName.Items.Count > 0)
            {
                if (Convert.ToInt32(cmbCompanyName.SelectedValue) > 0)
                {
                    MetricTargetPreset.CompanyId = Convert.ToByte(cmbCompanyName.Text);
                }
            }
            MetricTargetPreset.MetricTargetPresetName = txtPresetName.Text;
            MetricTargetPreset.MetricTargetPresetDesc = txtDescription.Text;
            MetricTargetPreset.dEMethod = cmbEType.Text;
            MetricTargetPreset.ValidationType = 0;
            MetricTargetPreset.IsSync1D = false;
            MetricTargetPreset.Validation_1D = false;
            MetricTargetPreset.Validation_4D = true;
            MetricTargetPreset.IsUsed = chkIsActive.Checked;
          
            MetricTargetPreset.ReferenceChartTypeId = Convert.ToInt32(cmbAlignmentChart.SelectedValue);

            MetricTargetPreset.MetricSumTolerances = new List<Entity.CPSyncV.MetricSum>(); //----To collect data for MetricSumTolerance
            for(int i = 0; i < lvMTPTolerances.Items.Count ; i++)
            {
                Label lID = (Label)lvMTPTolerances.Items[i].FindControl("lblMetricSumID");
                Label lName = (Label)lvMTPTolerances.Items[i].FindControl("lblToleranceName");
                TextBox tTolerance = (TextBox)lvMTPTolerances.Items[i].FindControl("txtTolerance");

                if (tTolerance.Text != "" && tTolerance.Text != "0")
                {
                    Entity.CPSyncV.MetricSum SumItem = new Entity.CPSyncV.MetricSum();
                    SumItem.MetricSumId = Convert.ToInt32(lID.Text);
                    SumItem.MetricSumName = lName.Text;

                    SumItem.TargetValue = Convert.ToDecimal(tTolerance.Text);
                    MetricTargetPreset.MetricSumTolerances.Add(SumItem);
                }
            }
            //---Add entry for PatchTolerance MetricSum if Patch Tolerance value is given for this Metric Target Preset----//
            if ((txtPatchTolerance1.Text != "" && txtPatchTolerance1.Text != "0") || (txtPatchTolerance2.Text != "" && txtPatchTolerance2.Text != "0") || (txtPatchTolerance3.Text != "" && txtPatchTolerance3.Text != "0") || (txtPatchTolerance4.Text != "" && txtPatchTolerance4.Text != "0") || (txtPatchTolerance5.Text != "" && txtPatchTolerance5.Text != "0"))
            {
                Entity.CPSyncV.MetricSum SumItemExtra = new Entity.CPSyncV.MetricSum();
                SumItemExtra.MetricSumId = 42;
                SumItemExtra.MetricSumName = "PatchTolerance";
                SumItemExtra.TargetValue = 0;
                MetricTargetPreset.MetricSumTolerances.Add(SumItemExtra);
            }

            //-----Items to add into PatchTolerance related tables-----//
            MetricTargetPreset.PatchTolerances = new List<Entity.CPSyncV.PatchTolerance>();
            if (cmbPatchSampleList1.SelectedIndex > 0 && txtPatchTolerance1.Text != "" && txtPatchTolerance1.Text != "0")
            {
                Entity.CPSyncV.PatchTolerance PatchItem1 = new Entity.CPSyncV.PatchTolerance();
                PatchItem1.SampleName = cmbPatchSampleList1.SelectedItem.Text;
                PatchItem1.Tolerance = Convert.ToDecimal(txtPatchTolerance1.Text);
                MetricTargetPreset.PatchTolerances.Add(PatchItem1);
            }
            if (cmbPatchSampleList2.SelectedIndex > 0 && txtPatchTolerance2.Text != "" && txtPatchTolerance2.Text != "0")
            {
                Entity.CPSyncV.PatchTolerance PatchItem2 = new Entity.CPSyncV.PatchTolerance();
                PatchItem2.SampleName = cmbPatchSampleList2.SelectedItem.Text;
                PatchItem2.Tolerance = Convert.ToDecimal(txtPatchTolerance2.Text);
                MetricTargetPreset.PatchTolerances.Add(PatchItem2);
            }
            if (cmbPatchSampleList3.SelectedIndex > 0 && txtPatchTolerance3.Text != "" && txtPatchTolerance3.Text != "0")
            {
                Entity.CPSyncV.PatchTolerance PatchItem3 = new Entity.CPSyncV.PatchTolerance();
                PatchItem3.SampleName = cmbPatchSampleList3.SelectedItem.Text;
                PatchItem3.Tolerance = Convert.ToDecimal(txtPatchTolerance3.Text);
                MetricTargetPreset.PatchTolerances.Add(PatchItem3);
            }
            if (cmbPatchSampleList4.SelectedIndex > 0 && txtPatchTolerance4.Text != "" && txtPatchTolerance4.Text != "0")
            {
                Entity.CPSyncV.PatchTolerance PatchItem4 = new Entity.CPSyncV.PatchTolerance();
                PatchItem4.SampleName = cmbPatchSampleList4.SelectedItem.Text;
                PatchItem4.Tolerance = Convert.ToDecimal(txtPatchTolerance4.Text);
                MetricTargetPreset.PatchTolerances.Add(PatchItem4);
            }
            if (cmbPatchSampleList5.SelectedIndex > 0 && txtPatchTolerance5.Text != "" && txtPatchTolerance5.Text != "0")
            {
                Entity.CPSyncV.PatchTolerance PatchItem5 = new Entity.CPSyncV.PatchTolerance();
                PatchItem5.SampleName = cmbPatchSampleList5.SelectedItem.Text;
                PatchItem5.Tolerance = Convert.ToDecimal(txtPatchTolerance5.Text);
                MetricTargetPreset.PatchTolerances.Add(PatchItem5);
            }

            int OriginalTotal = 0;
            OriginalTotal = MetricTargetPreset.PatchTolerances.Count;
            int AfterTotal = 0;
            MetricTargetPreset.PatchTolerances = MetricTargetPreset.PatchTolerances.GroupBy(i => i.SampleName).Select(ss => ss.FirstOrDefault()).ToList(); //Remove redudant data from user mistake
            AfterTotal = MetricTargetPreset.PatchTolerances.Count;
            if (AfterTotal < OriginalTotal)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recErrorSamePatchTol), false);
                return;
            }
            if (MetricTargetPreset.MetricSumTolerances.Count == 0 || MetricTargetPreset.PatchTolerances.Count == 0)
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recErrorTolMissing), false);
                return;
            }

            if (BId == 0) // Add New PatchPreset
            {
                _presenterMetricTargetPresetManagement.InsertMetricTargetPatchPreset();
                if (IsErrorOccured) return;
            }
            else // Edit PatchPreset
            {
                MetricTargetPreset.MetricTargetPresetID = BId;
                _presenterMetricTargetPresetManagement.UpdateMetricTargetPatchPreset();
                if (IsErrorOccured) return;
            }
            Response.Redirect("PatchPresetManagement.aspx");
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

        }

    }
}