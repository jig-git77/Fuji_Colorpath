using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl.AddProject
{
    public partial class ValidationSetup : BaseControl, IViewMeasurementSetting
    {
        public ValidationSetup()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Declaration
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private const string vsIsPostBack = "_IsPostBack";
        #endregion

        #region Page events
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState[vsIsPostBack] == null)
            {
                BindMeasurementDevice();
                BindMetricTargetPreset();
                LoadProjectInfoFromSession();
                ViewState[vsIsPostBack] = true;
            }
        }
        #endregion

        #region Control events
        protected void lnkbtnValidationSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.EnabledValidation = EnableValidationValue;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID = (short)MeasurementDeviceSelectedValue;
            SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(ValidationChartSelectedValue), cmbValidationChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset = new KeyValuePair<int, string>(Convert.ToInt32(cmbMetricTargetPreset.SelectedItem.Value), cmbMetricTargetPreset.SelectedItem.Text);

            //Load save control
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindValidationChart();
        }

        protected void cmbValidationChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayValidationChartImage();
        }


        #endregion

        #region Methods
        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MeasurementSetting != null)
            {
                EnableValidationValue = SessionManager.Instance.CurrentProject.EnabledValidation;
                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.HasValue && SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value > 0)
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID.Value.ToString();
                else
                    cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId.ToString();                                
                cmbMeasurementDevice_SelectedIndexChanged(null, null);

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key > 0)
                    cmbValidationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                else
                    cmbValidationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                cmbValidationChart_SelectedIndexChanged(null, null);

                if (SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key > 0)
                    cmbMetricTargetPreset.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Key.ToString();
            }

        }

        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            if (SessionManager.Instance.CurrentColorOrgRequest != null)
            {
                MeasurementDevices = MeasurementDevices.
                    Where(x => x.InstrumentTypeId == SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId).
                    ToList();
            }
            if (!IsErrorOccured)
            {
                cmbMeasurementDevice.DataValueField = "MeasurementDeviceId";
                cmbMeasurementDevice.DataTextField = "SerialNumber";
                cmbMeasurementDevice.DataSource = MeasurementDevices;
                cmbMeasurementDevice.DataBind();
                cmbMeasurementDevice_SelectedIndexChanged(null, null);
            }
        }

        private void BindValidationChart()
        {
            MeasurementDeviceId = MeasurementDeviceSelectedValue;
            ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.VerificationChart);
            _presenterMeasurementSetting.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbValidationChart.DataValueField = "ReferenceChartTypeID";
                cmbValidationChart.DataTextField = "ChartName";
                cmbValidationChart.DataSource = ReferenceChartType;
                cmbValidationChart.DataBind();
                cmbValidationChart.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void BindMetricTargetPreset()
        {
            _presenterMeasurementSetting.GetMetricTargetPreset();
            if (!IsErrorOccured)
            {
                cmbMetricTargetPreset.DataValueField = "MetricTargetPresetID";
                cmbMetricTargetPreset.DataTextField = "MetricTargetPresetName";
                cmbMetricTargetPreset.DataSource = MetricTargetPreset;
                cmbMetricTargetPreset.DataBind();
                cmbMetricTargetPreset.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private int MeasurementDeviceSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue.Trim()))
                    return Convert.ToInt32(cmbMeasurementDevice.SelectedValue.Trim());
                return 0;
            }
        }

        private int ValidationChartSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbValidationChart.SelectedValue.Trim()))
                    return Convert.ToInt32(cmbValidationChart.SelectedValue.Trim());
                return 0;
            }
        }
        private void DisplayValidationChartImage()
        {
            if (ValidationChartSelectedValue <= 0)
            {
                imgValidationChart.ImageUrl = ResolveUrl("../../Images/chart_preview.png");
            }
            else
            {
                imgValidationChart.ImageUrl = "ShowImage.aspx?id=" + ValidationChartSelectedValue;
            }
        }

        private bool EnableValidationValue
        {
            get
            {
                if (rbtnEnableValidationYes.Checked)
                    return true;
                else if (rbtnEnableValidationNo.Checked)
                    return false;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    rbtnEnableValidationYes.Checked = true;
                    rbtnEnableValidationNo.Checked = false;
                }
                else
                {
                    rbtnEnableValidationYes.Checked = false;
                    rbtnEnableValidationNo.Checked = true;
                }
            }
        }
        #endregion


        #region IViewMeasurementSetting members
        public List<Entity.Illuminant> Illumin { get; set; }

        public List<Entity.ObserverType> Observer { get; set; }

        public int DeviceId { get; set; }

        public List<Entity.MeasurementDevice> MeasurementDevices { get; set; }

        public int MeasurementDeviceId { get; set; }

        public List<Entity.ReferenceChartType> ReferenceChartType { get; set; }

        public short ReferenceChartTypeId { get; set; }

        public byte[] ReferenceChartTypeImage { get; set; }

        public int ProjectId { get; set; }

        public Entity.Project Project { get; set; }

        public int ChartDisplayType { get; set; }

        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset { get; set; }
        
        #endregion       
    }
}