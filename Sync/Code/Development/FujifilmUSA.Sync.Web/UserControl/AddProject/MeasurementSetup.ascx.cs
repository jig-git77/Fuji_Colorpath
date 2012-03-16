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
    public partial class MeasurementSetup : System.Web.UI.UserControl, IViewMeasurementSetting
    {
        #region "Declaration"
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        #endregion

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindMeasurementDevice();
            BindIlluminant();
            BindObserverType();
        }
        #endregion

        private void BindMeasurementDevice()
        {
            deviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            cmbMeasurementDevice.DataValueField = "Key";
            cmbMeasurementDevice.DataTextField = "Value";
            cmbMeasurementDevice.DataSource = Measurement;
            cmbMeasurementDevice.DataBind();
            cmbMeasurementDevice_SelectedIndexChanged(null, null);
        }

        private void BindIlluminant()
        {
            _presenterMeasurementSetting.GetIlluminant();
            cmbIlluminant.DataValueField = "IlluminantID";
            cmbIlluminant.DataTextField = "IlluminantType";
            cmbIlluminant.DataSource = Illumin;
            cmbIlluminant.DataBind();
        }

        private void BindObserverType()
        {
            _presenterMeasurementSetting.GetObserverType();
            cmbObserver.DataValueField = "ObserverTypeID";
            cmbObserver.DataTextField = "Type";
            cmbObserver.DataSource = Observer;
            cmbObserver.DataBind();

        }

        private void BindAlignmentChart()
        {
            MeasurementDeviceID = Convert.ToInt32(cmbMeasurementDevice.SelectedValue);
            _presenterMeasurementSetting.GetReferenceChartType();
            cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
            cmbAlignmentChart.DataTextField = "ChartName";
            cmbAlignmentChart.DataSource = ReferenceChartType;
            cmbAlignmentChart.DataBind();
        }

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin { get; set; }
        public List<ObserverType> Observer { get; set; }
        public int deviceId { get; set; }
        public Dictionary<int, string> Measurement { get; set; }
        public int MeasurementDeviceID { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        #endregion

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.MeasurementSetting = new MeasurementSetting()
            {

            };
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAlignmentChart();
        }

    }
}