using System;
using System.Linq;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class PreviewPrintCondition : BaseControl, IViewPreviewPrintCondition, IViewSaveWindow
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

        private PresenterPreviewPrintCondition _presenterPreviewPrintCondition;
        private PresenterSaveWindow _presenterSaveWindow;
        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        public PreviewPrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        protected override void OnInit(EventArgs e)
        {
            _presenterPreviewPrintCondition = new PresenterPreviewPrintCondition(this);
            _presenterSaveWindow = new PresenterSaveWindow(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            lnkbtnMeasurementSetup.Visible = IsXmfNewProject;
            LoadRecord();
        }

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            Response.Redirect(string.Format("~/COAddProject.aspx?id={0}", Guid.NewGuid()));
        }

        protected void lnkbtnSavePrintCondition_Click(object sender, EventArgs e)
        {
            if (IsXmfUser)
            {
                SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;                
                Project = SessionManager.Instance.CurrentProject;
                _presenterSaveWindow.SaveProject();
                _presenterSaveWindow.GetProjectDetail();

                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                    if (SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                        Response.Redirect("Export.aspx");
                    }
                    else if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                        Response.Redirect(string.Format("COAnalyze.aspx?id={0}", Guid.NewGuid()));
                    }
                    else
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                        //Response.Redirect(string.Format("COMeasurement.aspx?id={0}", Guid.NewGuid()));
                        Response.Redirect(string.Format("COMeasurement.aspx?id={0}&projectid={1}", Guid.NewGuid(), SessionManager.Instance.CurrentProject != null ? SessionManager.Instance.CurrentProject.ProjectId : 0));
                    }
                }
            }
        }

        protected void LoadRecord()
        {
            lblMedia.Text = SessionManager.Instance.CurrentProject.PrintCondition.Media;
            lblColorant.Text = SessionManager.Instance.CurrentProject.PrintCondition.Colorant;
            lblRipSettings.Text = SessionManager.Instance.CurrentProject.PrintCondition.RipSettings;
            lblNotes.Text = SessionManager.Instance.CurrentProject.PrintCondition.Notes;
            lblTechnicianName.Text = SessionManager.Instance.CurrentProject.PrintCondition.TechnicianName;
            lblSourceProfile.Text = SessionManager.Instance.CurrentProject.SourceProfile.ProfileName;

            DeviceFamilyProfilerOptionId = SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId;
            IlluminantId = SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant;
            ObserverTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType;
            ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
            MeasurementDeviceId = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId;
            V_MeasurementDeviceId = SessionManager.Instance.CurrentProject.MeasurementSetting.VMeasurementDeviceID;
            Opt_ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key;
            V_ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key;

            _presenterPreviewPrintCondition.GetPreviewPrintCondition();
            if (!IsErrorOccured)
            {
                lblMeasurementDevice.Text = Data["MeasurementDeviceId"];
                lblIlluminant.Text = Data["illuminantId"];
                lblObserver.Text = Data["observerTypeId"];
                lblAlignmentChart.Text = Data["referenceChartTypeId"];
                lblSelectedPresets.Text = Data["deviceFamilyProfilerOptionId"];
                lblPresetDescription.Text = Data["deviceFamilyProfilerOptionDesc"];
                imgChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(ReferenceChartTypeId) + "&Preview=1";
                lblVali_MeaDevice.Text = Data["V_MeasurementDeviceId"];
                if (Opt_ReferenceChartTypeId != 0)
                {
                    lblOptimizationChart.Text = Data["opt_referenceChartTypeId"];
                    Image_OptimizationChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key) + "&Preview=1";
                }
                if (V_ReferenceChartTypeId != 0)
                {
                    lblValiChart.Text = Data["v_referenceChartTypeId"];
                    Image_ValiChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key) + "&Preview=1";
                }
            }
            trUVSettings.Visible = lblMeasurementDevice.Text.Contains(
             Common.Utilities.GetEnumDescriptionAttributeValue(EInstrumentType.i1Sis));
            lblUVSettings.Text = SessionManager.Instance.CurrentProject.MeasurementSetting.UvIncluded
                                    ? Utilities.GetEnumDescriptionAttributeValue(EUVIncludedMode.UV)
                                    : Utilities.GetEnumDescriptionAttributeValue(EUVIncludedMode.UVCut);

            lblEnableValidation.Text = SessionManager.Instance.CurrentProject.EnabledValidation.ToString();
           
            if (SessionManager.Instance.CurrentProject.EnabledValidation)
            {
                Table_Validation.Visible = true;
                Div_ValidationChart.Visible = true;
                lblVali_Metric.Text = SessionManager.Instance.CurrentProject.MeasurementSetting.MetricTargetPreset.Value;
            }
        }

        public void SetImageSize()
        {
            double aw = 300;
            double ah = 255;
            double iw = 270;
            double ih = 400;

            double rw = aw / iw;
            double rh = ah / ih;
            double ratio;
            if (rw < rh)
            {
                ratio = rw;
            }
            else
            {
                ratio = rh;
            }
            int nw = Convert.ToInt32(iw * ratio);
            int nh = Convert.ToInt32(ih * ratio);

            imgChart.Width = System.Web.UI.WebControls.Unit.Pixel(nw);
            imgChart.Height = System.Web.UI.WebControls.Unit.Pixel(nh);
        }

        #region IViewPreviewPrintCondition Members
        public int DeviceFamilyProfilerOptionId { get; set; }
        public short IlluminantId { get; set; }
        public short ObserverTypeId { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short? MeasurementDeviceId { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public short? V_MeasurementDeviceId { get; set; }
        public short Opt_ReferenceChartTypeId { get; set; }
        public short V_ReferenceChartTypeId { get; set; }
        #endregion

        #region IViewSaveWindow Members
        public byte[] ChartPreview { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        #endregion
    }
}
