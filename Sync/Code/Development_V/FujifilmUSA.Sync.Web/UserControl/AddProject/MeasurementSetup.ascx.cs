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
        public bool IsXmfNewProject
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser) &&
                        SessionManager.Instance.CurrentProject.ProjectId == 0);
            }
        }

        private PresenterMeasurementSetting _presenterMeasurementSetting;
        private const string vsIsPostBack = "_IsPostBack";
        private const string _recSelectText = "selectText";
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

        public string AlignmentChartSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbAlignmentChart.SelectedValue.Trim()))
                    return cmbAlignmentChart.SelectedValue.Trim();
                return "0";
            }
        }

        public string OptimizationChartSelectedValue
        {
            get
            {
                if (!string.IsNullOrEmpty(cmbOptimizationChart.SelectedValue.Trim()))
                    return cmbOptimizationChart.SelectedValue.Trim();
                return "0";
            }
        }
        #endregion

        public MeasurementSetup()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lnkbtnProfilerOptions.Visible = IsXmfNewProject;
            if (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser) &&
                SessionManager.Instance.CurrentProject.ProjectId > 0)
            {
                lbtnMeasure.Visible = true;
                lnkbtnMeasurementSetup.Visible = false;
            }
            else
            {
                lbtnMeasure.Visible = false;
                lnkbtnMeasurementSetup.Visible = true;
            }

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

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.MeasurementSetting =
                new MeasurementSetting()
                {
                    Illuminant = Convert.ToInt16(cmbIlluminant.SelectedValue),
                    ObserverType = Convert.ToInt16(cmbObserver.SelectedValue),
                    MeasurementDeviceId = Convert.ToInt16(cmbMeasurementDevice.SelectedValue),
                    ReferenceChartType =
                        new KeyValuePair<short, string>(Convert.ToInt16(AlignmentChartSelectedValue),
                                                        cmbAlignmentChart.SelectedItem.Text),
                    UvIncluded =
                        cmbUVSettings.SelectedValue == Convert.ToInt32(EUVIncludedMode.UV).ToString() ? true : false,
                    OptReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(OptimizationChartSelectedValue), cmbOptimizationChart.SelectedItem.Text)

                };

            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;
            SessionManager.Instance.CurrentProject.LicenseTypeID = Convert.ToInt32(SessionManager.Instance.CurrentOperation);
            Project = SessionManager.Instance.CurrentProject;
            _presenterMeasurementSetting.SaveProject();

            if (!IsErrorOccured)
            {
                if(SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Verification)
                {
                    _presenterMeasurementSetting.GetProjectDetailWithVerification();
                }
                else
                {
                    _presenterMeasurementSetting.GetProjectDetail();
                }
                SessionManager.Instance.CurrentProject = Project;

                switch (SessionManager.Instance.CurrentProject.Status)
                {
                    case EOptimizeRunStatus.Open:
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                        Response.Redirect(string.Format(
                            SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                ? "COAnalyze.aspx?id={0}"
                                : "COMeasurement.aspx?id={0}", Guid.NewGuid()));
                        break;
                    case EOptimizeRunStatus.Closed:
                    case EOptimizeRunStatus.Certify:
                        ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                        _presenterMeasurementSetting.CreateOptimizeRun();
                        if (!IsErrorOccured)
                        {
                            _presenterMeasurementSetting.GetProjectDetail();
                            if (!IsErrorOccured)
                            {
                                SessionManager.Instance.CurrentProject = Project;
                                SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                                SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                                Response.Redirect(string.Format("COMeasurement.aspx?id={0}", Guid.NewGuid()));
                            }
                        }
                        break;
                }
            }
        }

        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ProfilerOption;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.MeasurementSetting =
                new MeasurementSetting()
                    {
                        Illuminant = Convert.ToInt16(cmbIlluminant.SelectedValue),
                        ObserverType = Convert.ToInt16(cmbObserver.SelectedValue),
                        MeasurementDeviceId = Convert.ToInt16(cmbMeasurementDevice.SelectedValue),
                        ReferenceChartType =
                            new KeyValuePair<short, string>(Convert.ToInt16(AlignmentChartSelectedValue),
                                                            cmbAlignmentChart.SelectedItem.Text),
                        UvIncluded =
                            cmbUVSettings.SelectedValue == Convert.ToInt32(EUVIncludedMode.UV).ToString() ? true : false,
                        OptReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(OptimizationChartSelectedValue), cmbOptimizationChart.SelectedItem.Text)
                    };

            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.ValidationSetup;
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

        protected void cmbOptimizationChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayOptimizationChartImage();
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
                cmbAlignmentChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString();
                cmbAlignmentChart_SelectedIndexChanged(null, null);
                cmbOptimizationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                cmbOptimizationChart_SelectedIndexChanged(null, null);
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

        /// <summary>
        /// Method to bind combo of Optimization Chart under Charts section in Measurement Setup
        /// </summary>
        private void BindOptimizationChart()
        {
            ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.OptimizationChart);
            _presenterMeasurementSetting.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbOptimizationChart.DataValueField = "ReferenceChartTypeID";
                cmbOptimizationChart.DataTextField = "ChartName";
                cmbOptimizationChart.DataSource = ReferenceChartType;
                cmbOptimizationChart.DataBind();
                cmbOptimizationChart.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));
            }
        }

        /// <summary>
        /// Method to bind combo Base DLP Chart under Charts section in Measurement Setup
        /// </summary>
        private void BindBaseDLPChart()
        {
            ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.ProfileChart);
            _presenterMeasurementSetting.GetReferenceChartType();

            if (!IsErrorOccured)
            {
                cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
                cmbAlignmentChart.DataTextField = "ChartName";
                cmbAlignmentChart.DataSource = ReferenceChartType;
                cmbAlignmentChart.DataBind();
                cmbAlignmentChart.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select -- 
            }
        }

        private void BindAlignmentChart()
        {
            if (string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue)) return;
            MeasurementDeviceId = Convert.ToInt32(cmbMeasurementDevice.SelectedValue);

            #region Code not used now as it converted into smaller functions -> BindBaseDLPChart() and BindOptimizationChart()
            //ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.ProfileChart);                     //------------If we are in initial project setup-------------//
            //if (SessionManager.Instance.CurrentProject != null)
            //{
            //    if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.First().HasProfile)
            //    {
            //        ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.OptimizationChart);              //---------------If we are in optimization run--------------//
            //    }
            //    else
            //    {
            //        ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.ProfileChart);                  //------------If we are in initial project setup-------------//
            //    }
            //}

            //ChartDisplayType = Convert.ToInt32(EReferenceChartDisType.OptimizationChart);     //--------By default, just till fix silverlight issue

            //_presenterMeasurementSetting.GetReferenceChartType();
            //if (!IsErrorOccured)
            //{
            //    cmbAlignmentChart.DataValueField = "ReferenceChartTypeID";
            //    cmbAlignmentChart.DataTextField = "ChartName";
            //    cmbAlignmentChart.DataSource = ReferenceChartType;
            //    cmbAlignmentChart.DataBind();
            //    cmbAlignmentChart.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));//-- Select --

            //    cmbOptimizationChart.DataValueField = "ReferenceChartTypeID";
            //    cmbOptimizationChart.DataTextField = "ChartName";
            //    cmbOptimizationChart.DataSource = ReferenceChartType;
            //    cmbOptimizationChart.DataBind();
            //    cmbOptimizationChart.Items.Insert(0, new ListItem(GetResourceString(_recSelectText), "0"));
            //} 
            #endregion

            BindBaseDLPChart();
            BindOptimizationChart();

            if (SessionManager.Instance.CurrentColorOrgRequest != null)
            {
                //Load selected chart if we are in update mode
                cmbAlignmentChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString();
                cmbAlignmentChart.Enabled = false;

                //Load selected chart if we are in update mode
                cmbOptimizationChart.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                cmbOptimizationChart.Enabled = false;
            }
            DisplayChartImage();
            DisplayOptimizationChartImage();
        }

        private void DisplayChartImage()
        {
            if (Convert.ToInt32(AlignmentChartSelectedValue) <= 0)
            {
                imgChart.ImageUrl = ResolveUrl("../../Images/chart_preview.png");
            }
            else
            {
                imgChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(AlignmentChartSelectedValue);
            }
        }

        private void DisplayOptimizationChartImage()
        {
            if (Convert.ToInt32(OptimizationChartSelectedValue) <= 0)
            {
                imgOptimizationChart.ImageUrl = ResolveUrl("../../Images/chart_preview.png");
            }
            else
            {
                imgOptimizationChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(OptimizationChartSelectedValue);
            }
        }

        private void BindUVSettings()
        {
            cmbUVSettings.DataValueField = "Key";
            cmbUVSettings.DataTextField = "Value";
            cmbUVSettings.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EUVIncludedMode>();
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
        public short ReferenceChartTypeId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int ChartDisplayType { get; set; }
        #endregion
    }
}
