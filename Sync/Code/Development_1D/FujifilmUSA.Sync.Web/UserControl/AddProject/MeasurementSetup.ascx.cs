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

        #region Private Variables - Globalization
        private const string _recrfvcMeasurementDeviceContainer = "rfvcMeasurementDeviceContainer";
        private const string _recrfvcAlignmentChartContainer = "rfvcAlignmentChartContainer";
        private const string _recrfvcOptimizationChartContainer = "rfvcOptimizationChartContainer";
        #endregion

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
                BindMeasurementDevice();           
                BindMeasurementCount(false);
                BindIlluminant();
                BindObserverType();
                BindDensityType(); //Load DensityType 
                ViewState[vsIsPostBack] = true;
                LoadProjectInfoFromSession();
            }

            //Set localized text
            rfvcMeasurementDeviceContainer.ValidationMsg = GetResourceString(_recrfvcMeasurementDeviceContainer);
            rfvcAlignmentChartContainer.ValidationMsg = GetResourceString(_recrfvcAlignmentChartContainer);
            rfvcOptimizationChartContainer.ValidationMsg = GetResourceString(_recrfvcOptimizationChartContainer);
        }
        #endregion

        #region Control Events

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant = Convert.ToInt16(cmbIlluminant.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType = Convert.ToInt16(cmbObserver.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId = Convert.ToInt16(cmbMeasurementDevice.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(AlignmentChartSelectedValue), cmbAlignmentChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.MeasurementSetting.UvIncluded = cmbUVSettings.SelectedValue == Convert.ToInt32(EUVIncludedMode.UV).ToString() ? true : false;
            SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(OptimizationChartSelectedValue), cmbOptimizationChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;
            Project = SessionManager.Instance.CurrentProject;
            _presenterMeasurementSetting.SaveProject();

            if (!IsErrorOccured)
            {
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
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
                        if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                            _presenterMeasurementSetting.Create1DOptimizeRun();
                        else
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
            //If project is new then call constructor of MeasurementSetting
            if (SessionManager.Instance.CurrentProject.ProjectId <= 0)
                SessionManager.Instance.CurrentProject.MeasurementSetting = new MeasurementSetting();

            #region Assigning values to entity
            SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant = Convert.ToInt16(cmbIlluminant.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType = Convert.ToInt16(cmbObserver.SelectedValue);
            SessionManager.Instance.CurrentProject.DensityType = (DensityTypes)Convert.ToInt16(cmbDensityType.SelectedValue);  //DensityType Selection 
            SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId = Convert.ToInt16(cmbMeasurementDevice.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType =
                new KeyValuePair<short, string>(Convert.ToInt16(AlignmentChartSelectedValue),
                                                cmbAlignmentChart.SelectedItem.Text);
            SessionManager.Instance.CurrentProject.MeasurementSetting.UVMode = short.Parse(cmbUVSettings.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementCount = short.Parse(cmbMeasurementCount.SelectedValue);
            SessionManager.Instance.CurrentProject.MeasurementSetting.ReadMode = (trReadMode.Visible == true ? cmbReadMode.SelectedValue : null);

            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
            {
                SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(AlignmentChartSelectedValue),
                                                cmbAlignmentChart.SelectedItem.Text);               
            }
            else
                SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType = new KeyValuePair<short, string>(Convert.ToInt16(OptimizationChartSelectedValue), cmbOptimizationChart.SelectedItem.Text);
            #endregion

            //Set next navigation level as ValidationSetup
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
            SetOptimizationChart();           
        }

        protected void cmbOptimizationChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayOptimizationChartImage();
        }

        protected void cmbReadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                                || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point")
            {
                BindMeasurementCount(true);
            }
            else
            {
                BindMeasurementCount(false);
            }
            trMeasCount.Visible = ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                                || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point");
        }

        protected void cmbUVSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                                || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2))
            {
                BindReadMode();
            }
        }
        #endregion

        #region Methods        

        private void LoadProjectInfoFromSession()
        {
            if (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentProject.MeasurementSetting != null)
            {
                cmbIlluminant.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant.ToString();
                cmbObserver.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType.ToString();
                cmbDensityType.SelectedValue = Convert.ToInt16(SessionManager.Instance.CurrentProject.DensityType).ToString(); //DensityType Selection
                cmbMeasurementDevice.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId.ToString();
               
                cmbMeasurementDevice_SelectedIndexChanged(null, null);

                cmbAlignmentChart.SelectedIndex = cmbAlignmentChart.Items.IndexOf(cmbAlignmentChart.Items.FindByValue(SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString()));
                if (cmbAlignmentChart.SelectedIndex == 0 && cmbAlignmentChart.Items.Count > 0)
                    cmbAlignmentChart.SelectedIndex = 1;                
                cmbAlignmentChart_SelectedIndexChanged(null, null);

                cmbOptimizationChart.SelectedIndex = cmbOptimizationChart.Items.IndexOf(cmbOptimizationChart.Items.FindByValue(SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString()));
                if (cmbOptimizationChart.SelectedIndex == 0 && cmbOptimizationChart.Items.Count > 0)
                    cmbOptimizationChart.SelectedIndex = 1;                
                cmbOptimizationChart_SelectedIndexChanged(null, null);
                             
                cmbUVSettings.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.UVMode.ToString();             
                BindReadMode();
                if (SessionManager.Instance.CurrentProject.MeasurementSetting.ReadMode != null)
                {
                    cmbReadMode.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.ReadMode.ToString();
                }
                if (SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementCount > 0)
                {
                    BindMeasurementCount(true);
                    cmbMeasurementCount.SelectedValue = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementCount.ToString();
                    trMeasCount.Visible = true;
                }
            }
        }

        private void BindMeasurementDevice()
        {
            if (cmbMeasurementDevice.DataSource != null) return;

            DeviceId = SessionManager.Instance.CurrentDevice.DeviceId;
            _presenterMeasurementSetting.GetMeasurementDevice();
            if (SessionManager.Instance.CurrentColorOrgRequest != null)
            {
                if (SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId == 1) //I1-Pro and I1-Pro2
                {
                    MeasurementDevices = MeasurementDevices.Where(x => x.InstrumentTypeId == 1 || x.InstrumentTypeId == 4).ToList();
                }
                else if (SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId == 3)  //I1-IO and I1-IO-Pro2
                {
                    MeasurementDevices = MeasurementDevices.Where(x => x.InstrumentTypeId == 3 || x.InstrumentTypeId == 5).ToList();
                }
                else  //I1-Sis
                {
                    MeasurementDevices = MeasurementDevices.Where(x => x.InstrumentTypeId == SessionManager.Instance.CurrentColorOrgRequest.MeasurementDeviceTypeId).ToList();
                }
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

        private void BindDensityType()
        {
            if (cmbDensityType.DataSource != null) return;

            cmbDensityType.DataValueField = "Key";
            cmbDensityType.DataTextField = "Value";
            cmbDensityType.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<DensityTypes>();
            cmbDensityType.DataBind();
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
            if (SessionManager.Instance.CurrentProject != null && (ProjectTypeID == Convert.ToInt32(EProjectType.G7) || ProjectTypeID == Convert.ToInt32(EProjectType.ISO)))   //----Pass MetricTargetPresetId to get list of suitable reference charts
            {
                MetricTargetPresetID = SessionManager.Instance.CurrentProject.MetricTargetPresetId;
            }
            BindBaseDLPChart();
            BindOptimizationChart();

            if (SessionManager.Instance.CurrentColorOrgRequest != null)
            {
                //Load selected chart if we are in update mode                
                cmbAlignmentChart.SelectedIndex = cmbAlignmentChart.Items.IndexOf(cmbAlignmentChart.Items.FindByValue(SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString()));
                if (cmbAlignmentChart.SelectedIndex == 0 && cmbAlignmentChart.Items.Count > 0)
                    cmbAlignmentChart.SelectedIndex = 1;
                cmbAlignmentChart.Enabled = false;

                //Load selected chart if we are in update mode
                cmbOptimizationChart.SelectedIndex = cmbOptimizationChart.Items.IndexOf(cmbOptimizationChart.Items.FindByValue(SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString()));
                if (cmbOptimizationChart.SelectedIndex == 0 && cmbOptimizationChart.Items.Count > 0)
                    cmbOptimizationChart.SelectedIndex = 1;
                cmbOptimizationChart.Enabled = false;
            }
            DisplayChartImage();
            DisplayOptimizationChartImage();
            SetOptimizationChart();
        }

        private void SetOptimizationChart()
        {
            if (cmbAlignmentChart.SelectedIndex != 0 && cmbOptimizationChart.SelectedIndex == 0)
            {
                cmbOptimizationChart.SelectedIndex = cmbOptimizationChart.Items.IndexOf(cmbOptimizationChart.Items.FindByValue(cmbAlignmentChart.SelectedValue));
                DisplayOptimizationChartImage();
            }
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
        
            if (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] ==
                                    (EInstrumentType.i1Sis))
            {
                cmbUVSettings.DataSource = Common.Utilities.GetEnumDescriptionAttributeDropdown<EUVIncludedMode>().Where(p => p.Value != Common.Utilities.GetEnumDescriptionAttributeValue(EUVIncludedMode.UVPlus)).ToDictionary(p => p.Key, p => p.Value);
            }
            cmbUVSettings.DataBind();
        }

        private void SetUVSettings()
        {
            if (string.IsNullOrEmpty(cmbMeasurementDevice.SelectedValue) || MeasurementDevicesView == null)
            {
                trUVSettings.Visible = false;
                trReadMode.Visible = false;
                trMeasCount.Visible = false;
                return;
            }
            trUVSettings.Visible = (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Sis) 
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Pro2) 
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.eXact)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1Sis2));
            trReadMode.Visible = (MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)                               
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2));
            
            trMeasCount.Visible = ((MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IO)
                                    || MeasurementDevicesView[Convert.ToInt32(cmbMeasurementDevice.SelectedValue)] == (EInstrumentType.i1IOPro2)) && cmbReadMode.SelectedValue == "Point");
            BindUVSettings();           
        }

        private void BindMeasurementCount(bool IsPro2Type)
        {
            cmbMeasurementCount.Items.Clear();
            if (IsPro2Type)
            {
                //----1 to 5 for Patch reading for i1-Pro2 instrument type
                cmbMeasurementCount.Items.Add(new ListItem("1", "1"));
                cmbMeasurementCount.Items.Add(new ListItem("2", "2"));
                cmbMeasurementCount.Items.Add(new ListItem("3", "3"));
                cmbMeasurementCount.Items.Add(new ListItem("4", "4"));
                cmbMeasurementCount.Items.Add(new ListItem("5", "5"));
            }
            else
                //-----0 for Strip reading for all other instruments type
                cmbMeasurementCount.Items.Add(new ListItem("0", "0"));
        }

        private void BindReadMode()
        {
            cmbReadMode.Items.Clear();
            cmbReadMode.Items.Add(new ListItem("Strip", "Strip"));
            cmbReadMode.Items.Add(new ListItem("Point", "Point"));

            cmbReadMode_SelectedIndexChanged(null, null);
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
        public List<Entity.CPSyncV.MetricTargetPreset> MetricTargetPreset { get; set; }     
        public ELicenseType LicenseType { get; set; }

        public int ProjectTypeID
        {
            get
            {               
                return SessionManager.Instance.CurrentProject.ProjectTypeId;
            }
        }
       
        public int MetricTargetPresetID { get; set; }
        public Dictionary<int, string> ReTargetProjectList { get; set; }
        public byte[] ChartPreview { get; set; }
        #endregion
                      
    }
}
