using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementSetup : BaseControl, IViewMeasurementSetting
    {
        #region "Declaration"
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private string vsIsPostBack = "_IsPostBack";
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState[vsIsPostBack] == null)
            {
                BindMeasurementDevice();
                BindIlluminant();
                BindObserverType();
                ViewState[vsIsPostBack] = true;
            }
        }
        #endregion

        #region Control Events

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.MeasurementSetting =
                new MeasurementSetting()
                    {
                        Illuminant = Convert.ToInt16(cmbIlluminant.SelectedValue),
                        ObserverType = Convert.ToInt16(cmbObserver.SelectedValue),
                        MeasurementDeviceId = Convert.ToInt16(cmbMeasurementDevice.SelectedValue),
                        ReferenceChartTypeId = Convert.ToInt16(cmbAlignmentChart.SelectedValue)
                    };
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAlignmentChart();
        }

        protected void cmbAlignmentChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChartImage();
        }

        #endregion

        #region Methods
        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            cmbMeasurementDevice.DataValueField = "Key";
            cmbMeasurementDevice.DataTextField = "Value";
            cmbMeasurementDevice.DataSource = Measurement;
            cmbMeasurementDevice.DataBind();
            cmbMeasurementDevice_SelectedIndexChanged(null, null);
        }

        private void BindIlluminant()
        {
            if (cmbIlluminant.DataSource != null) return;

            _presenterMeasurementSetting.GetIlluminant();
            cmbIlluminant.DataValueField = "IlluminantID";
            cmbIlluminant.DataTextField = "IlluminantType";
            cmbIlluminant.DataSource = Illumin;
            cmbIlluminant.DataBind();
        }

        private void BindObserverType()
        {
            if (cmbObserver.DataSource != null) return;

            _presenterMeasurementSetting.GetObserverType();
            cmbObserver.DataValueField = "ObserverTypeID";
            cmbObserver.DataTextField = "Type";
            cmbObserver.DataSource = Observer;
            cmbObserver.DataBind();
        }

        private void BindAlignmentChart()
        {
            MeasurementDeviceId = Convert.ToInt32(cmbMeasurementDevice.SelectedValue);
            _presenterMeasurementSetting.GetReferenceChartType();
            cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
            cmbAlignmentChart.DataTextField = "ChartName";
            cmbAlignmentChart.DataSource = ReferenceChartType;
            cmbAlignmentChart.DataBind();
            cmbAlignmentChart.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private void DisplayChartImage()
        {
            imgChart.ImageUrl = "ShowImage.aspx?id="+ Convert.ToString(cmbAlignmentChart.SelectedValue);
        }
        #endregion

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin { get; set; }
        public List<ObserverType> Observer { get; set; }
        public int DeviceId { get; set; }
        public Dictionary<int, string> Measurement { get; set; }
        public int MeasurementDeviceId { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        public byte[] ReferenceChartTypeImage { get; set; }
        public int ReferenceChartTypeId { get; set; }
        #endregion
    }
}