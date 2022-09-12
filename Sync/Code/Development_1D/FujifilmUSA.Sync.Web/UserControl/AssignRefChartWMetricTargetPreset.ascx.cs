using FujifilmUSA.Sync.Entity.CPSyncV;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AssignRefChartWMetricTargetPreset : BaseControl, IViewReferenceChartPreset
    {

        #region "Declaration"
        private PresenterReferenceChartPreset _PresenterReferenceChartPreset;
        #endregion

        #region IViewReferenceChartPreset Member
        public Dictionary<int, string> lstReferenceChartTypeID { get; set; }
        public Dictionary<int, string> lstMetricTargetPresetID { get; set; }
        public List<int> lstReferenceChartPresetId { get; set; }
        public List<ReferenceChartPreset> lstReferenceChartPreset { get; set; }
        public int ReferenceChartTypeID { get; set; }
        #endregion

        #region Page Methods

        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);
            if (!IsPostBack)
            {
                BindReferenceChartTypeID();
            }
        }

        /// <summary>
        /// OnInit
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            _PresenterReferenceChartPreset = new PresenterReferenceChartPreset(this);
        }

        #endregion

        #region Click Events

        /// <summary>
        /// lbtnSave_Click
        /// </summary>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            fujiStatusbar.DisplayMessage(Common.Utilities.Fields_Marked_With_Are_Required, true);

            //To select ReferenceChartTypeID 
            if (cmbReferenceChartTypeID.SelectedIndex == 0)
            {
                fujiStatusbar.DisplayMessage("Select Reference Chart.", false);
                return;
            }

            lstReferenceChartPreset = new List<ReferenceChartPreset>();

            foreach (ListItem item in chklistMetricTargetPresetID.Items)
            {
                if (item.Selected)
                { lstReferenceChartPreset.Add(new ReferenceChartPreset { MetricTargetPresetID = Convert.ToInt32(item.Value) }); }
            }

            //To check atlest one MetricTargetPresetID is selected
            if (lstReferenceChartPreset.Count == 0)
            {
                fujiStatusbar.DisplayMessage("Select Metric Target Preset.", false);
                return;
            }

            ReferenceChartTypeID = Convert.ToInt32(cmbReferenceChartTypeID.SelectedValue);

            //If any error reply with error
            if (_PresenterReferenceChartPreset.InsertReferenceChartPreset() == -1)
            {
                fujiStatusbar.DisplayMessage("Error While Creating ReferenceChartPresets.", false);
                return;
            }
            else
            { fujiStatusbar.DisplayMessage("Reference Chart Presets Created.", true); }

        }

        #endregion

        #region Other Events

        /// <summary>
        /// cmbReferenceChartTypeID_SelectedIndexChanged
        /// </summary>
        protected void cmbReferenceChartTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //To check ReferenceChartTypeID is selected
            if (cmbReferenceChartTypeID.SelectedValue != null && cmbReferenceChartTypeID.SelectedValue != "")
            {
                ReferenceChartTypeID = Convert.ToInt32(cmbReferenceChartTypeID.SelectedValue);
                BindlistMetricTargetPresetID();
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// BindSourceProfileDropDown
        /// </summary>
        private void BindReferenceChartTypeID()
        {
            cmbReferenceChartTypeID.Items.Clear();

            _PresenterReferenceChartPreset.GetReferenceChartType();
            if (!IsErrorOccured)
            {
                cmbReferenceChartTypeID.DataTextField = "Value";
                cmbReferenceChartTypeID.DataValueField = "Key";
                cmbReferenceChartTypeID.DataSource = lstReferenceChartTypeID;
                cmbReferenceChartTypeID.DataBind();
            }

            cmbReferenceChartTypeID.Items.Insert(0, new ListItem("--Select Reference Chart Type--", "0"));//-- Select Source Profile --
        }

        /// <summary>
        /// BindlistMetricTargetPresetID
        /// </summary>
        private void BindlistMetricTargetPresetID()
        {
            chklistMetricTargetPresetID.Items.Clear();
            _PresenterReferenceChartPreset.GetMetricTargetPreset();

            if (!IsErrorOccured)
            {
                chklistMetricTargetPresetID.DataTextField = "Value";
                chklistMetricTargetPresetID.DataValueField = "Key";
                chklistMetricTargetPresetID.DataSource = lstMetricTargetPresetID;
                chklistMetricTargetPresetID.DataBind();

                _PresenterReferenceChartPreset.GetReferenceChartPreset();

                if (!IsErrorOccured)
                    if (lstReferenceChartPresetId.Count > 0)
                        // To select checkbox from databased
                        foreach (ListItem item in chklistMetricTargetPresetID.Items)
                        {
                            if (lstReferenceChartPresetId.Contains(Convert.ToInt32(item.Value)))
                            {
                                item.Selected = true;
                            }
                        }
            }
        }

        #endregion
    }
}