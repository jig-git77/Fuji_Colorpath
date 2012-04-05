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
        private const string vsIsPostBack = "_IsPostBack";

        private Dictionary<int, EInstrumentType> MeasurementDevicesView
        {
            get
            {
                if (ViewState["vsMeasurementDevice"] == null)
                    return new Dictionary<int, EInstrumentType>();
                else
                    return (Dictionary<int, EInstrumentType>)ViewState["vsMeasurementDevice"];
            }
            set
            {
                ViewState["vsMeasurementDevice"] = value;
            }
        }

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
                BindUVSettings();
                BindMeasurementDevice();
                BindIlluminant();
                BindObserverType();
                ViewState[vsIsPostBack] = true;
                LoadProjectInfoFromSession();
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
                        ReferenceChartType =
                            new KeyValuePair<short, string>(Convert.ToInt16(cmbAlignmentChart.SelectedValue),
                                                            cmbAlignmentChart.SelectedItem.Text),
                        UvIncluded =
                            cmbUVSettings.SelectedValue == Convert.ToInt32(EUVIncludedMode.UV).ToString() ? true : false
                    };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }

        protected void cmbMeasurementDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAlignmentChart();
            SetUVSettings();
        }

        protected void cmbAlignmentChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayChartImage();
        }

        #endregion

        #region Methods

        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MeasurementSetting != null)
            {
                cmbIlluminant.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant.ToString();
                cmbObserver.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType.ToString();
                cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId.ToString();
                cmbUVSettings.SelectedValue =
                    SessionManager.Instance.CurrentProject.MeasurementSetting.UvIncluded
                        ? Convert.ToInt32(EUVIncludedMode.UV).ToString()
                        : Convert.ToInt32(EUVIncludedMode.UVCut).ToString();
                cmbMeasurementDevice_SelectedIndexChanged(null, null);
                cmbAlignmentChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.ToString();
                cmbAlignmentChart_SelectedIndexChanged(null, null);

                //cmbIlluminant.Enabled = cmbObserver.Enabled = cmbMeasurementDevice.Enabled =cmbAlignmentChart.Enabled =
                //  !(SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 0
                //   );
            }
        }

        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            if (!IsErrorOccured)
            {
                cmbMeasurementDevice.DataValueField = "MeasurementDeviceId";
                cmbMeasurementDevice.DataTextField = "SerialNumber";
                cmbMeasurementDevice.DataSource = MeasurementDevices;
                cmbMeasurementDevice.DataBind();
                MeasurementDevicesView = MeasurementDevices.Select(x => new KeyValuePair<int, EInstrumentType>(
                                                                            x.MeasurementDeviceId,
                                                                            (EInstrumentType)
                                                                            Enum.Parse(typeof(EInstrumentType),
                                                                                       x.InstrumentTypeId.ToString()))).
                    ToDictionary(x => x.Key, x => x.Value);
                cmbMeasurementDevice_SelectedIndexChanged(null, null);
            }
        }

        private void BindIlluminant()
        {
            if (cmbIlluminant.DataSource != null) return;

            _presenterMeasurementSetting.GetIlluminant();
            if (!IsErrorOccured)
            {
                cmbIlluminant.DataValueField = "IlluminantID";
                cmbIlluminant.DataTextField = "IlluminantType";
                cmbIlluminant.DataSource = Illumin;
                cmbIlluminant.DataBind();
            }
        }

        private void BindObserverType()
        {
            if (cmbObserver.DataSource != null) return;

            _presenterMeasurementSetting.GetObserverType();
            if (!IsErrorOccured)
            {
                cmbObserver.DataValueField = "ObserverTypeID";
                cmbObserver.DataTextField = "Type";
                cmbObserver.DataSource = Observer;
                cmbObserver.DataBind();
            }
        }

        private void BindAlignmentChart()
        {
            if (string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue)) return;
            MeasurementDeviceId = Convert.ToInt32(cmbMeasurementDevice.SelectedValue);
            _presenterMeasurementSetting.GetReferenceChartType();
            if (!IsErrorOccured)
            {
                cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
                cmbAlignmentChart.DataTextField = "ChartName";
                cmbAlignmentChart.DataSource = ReferenceChartType;
                cmbAlignmentChart.DataBind();
                cmbAlignmentChart.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
        }

        private void DisplayChartImage()
        {
            imgChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(cmbAlignmentChart.SelectedValue);
        }

        private void BindUVSettings()
        {
            cmbUVSettings.DataValueField = "Key";
            cmbUVSettings.DataTextField = "Value";
            cmbUVSettings.DataSource = Common.Utilities.GetEnumDropdown<EUVIncludedMode>();
            cmbUVSettings.DataBind();
        }

        private void SetUVSettings()
        {
            if (string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue) || MeasurementDevicesView == null)
            {
                trUVSettings.Visible = false;
                return;
            }
            trUVSettings.Visible = (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] ==
                                    EInstrumentType.i1Sis);
        }

        #endregion

        #region IViewMeasurementSetting Members
        public List<Illuminant> Illumin { get; set; }
        public List<ObserverType> Observer { get; set; }
        public int DeviceId { get; set; }
        public List<MeasurementDevice> MeasurementDevices { get; set; }
        public int MeasurementDeviceId { get; set; }
        public List<ReferenceChartType> ReferenceChartType { get; set; }
        public byte[] ReferenceChartTypeImage { get; set; }
        public int ReferenceChartTypeId { get; set; }
        #endregion
    }
}