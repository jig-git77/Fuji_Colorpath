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
    public partial class PreviewPrintCondition : BaseControl, IViewPreviewPrintCondition
    {
        #region "Declaration"
        private PresenterPreviewPrintCondition _presenterPreviewPrintCondition;
        #endregion

        protected override void OnInit(EventArgs e)
        {
            _presenterPreviewPrintCondition = new PresenterPreviewPrintCondition(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadRecord();
        }

        protected void LoadRecord()
        {
            lblMedia.Text = SessionManager.Instance.CurrentProject.PrintCondition.Media;
            lblColorant.Text = SessionManager.Instance.CurrentProject.PrintCondition.Colorant;
            lblRipSettings.Text = SessionManager.Instance.CurrentProject.PrintCondition.RipSettings;
            lblNotes.Text = SessionManager.Instance.CurrentProject.PrintCondition.Notes;
            lblTechnicianName.Text = SessionManager.Instance.CurrentProject.PrintCondition.TechnicianName;

            DeviceFamilyProfilerOptionId = SessionManager.Instance.CurrentProject.DeviceFamilyProfilerOptionId;
            IlluminantId = SessionManager.Instance.CurrentProject.MeasurementSetting.Illuminant;
            ObserverTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ObserverType;
            ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
            MeasurementDeviceId = SessionManager.Instance.CurrentProject.MeasurementSetting.MeasurementDeviceId;
            _presenterPreviewPrintCondition.GetPreviewPrintCondition();
            if (!IsErrorOccured)
            {
                lblMeasurementDevice.Text = Data["MeasurementDeviceId"];
                lblIlluminant.Text = Data["illuminantId"];
                lblObserver.Text = Data["observerTypeId"];
                lblAlignmentChart.Text = Data["referenceChartTypeId"];
                lblSelectedPresets.Text = Data["deviceFamilyProfilerOptionId"];
                lblPresetDescription.Text = Data["deviceFamilyProfilerOptionDesc"];
                imgChart.ImageUrl = "ShowImage.aspx?id=" + Convert.ToString(ReferenceChartTypeId);
            }
        }

        #region IViewPreviewPrintCondition Members
        public int DeviceFamilyProfilerOptionId { get; set; }
        public short IlluminantId { get; set; }
        public short ObserverTypeId { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short? MeasurementDeviceId { get; set; }
        public Dictionary<string, string> Data { get; set; }
        #endregion
    }
}