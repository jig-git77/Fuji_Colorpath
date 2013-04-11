using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Export : BaseControl, IViewExport
    {
        #region Private Variables - Globalization
        private const string _recDownload = "downloadText";
        private const string _recDLP = "dlpText";
        private const string _recDownloadChartNotAvailable = "downloadChartNotAvailableText";
        #endregion

        #region Construtor
        public Export()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        } 
        #endregion

        private PresenterExport _presenterExport;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            _presenterExport = new PresenterExport(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc");
                lbtnMeasure.Visible = (SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.Edit);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                {
                    trExportViewForSync1D.Visible = true;
                    trAdvancedDownload.Visible = false;
                    BindCurve();
                }
                else
                {
                    trAdvancedDownload.Visible = true;
                }
            }
        }

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                _presenterExport.Create1DOptimizeRun();
            else
                _presenterExport.CreateOptimizeRun();
            if (!IsErrorOccured)
            {
                _presenterExport.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                    Response.Redirect("Measurement.aspx");
                }
            }
        }

        protected void lbtnDownloadDLP_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                _presenterExport.Get1DDeviceLinkProfile();
            else
                _presenterExport.GetDeviceLinkProfile();
            string DLPName = SessionManager.Instance.CurrentOptimizeRun.DlpName;
            if (SessionManager.Instance.CurrentOptimizeRun.EndDateTime.HasValue)
            {
                DLPName = SessionManager.Instance.CurrentProject.ProjectName + " "+ GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc";
            }
            if (!IsErrorOccured)
            {
                if (DeviceLinkProfile != null)
                    Utilities.DownloadFile(DeviceLinkProfile, string.Format("{0}", DLPName));
            }
        }

        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            //ReferenceChartTypeId = SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Key;  
            //Here we are considering optimization chart as export will be visible only once DLP is created (i.e. success of Run0)
            ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key;  
            _presenterExport.GetChartPreview();
            if (!IsErrorOccured)
            {
                if (ChartPreview != null)
                    Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Value), ".pdf");
                else
                    fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
            }
            else
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
            }
        }

        protected void lbtnDownloadDestinationICC_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetDestinationProfile();
            if (!IsErrorOccured)
            {
                if (SourceProfile != null)
                    Utilities.DownloadFile(SourceProfile,
                                           string.Format("{0}.icc",
                                                         SessionManager.Instance.CurrentProject.ProjectName));
            }
        }

        private void BindCurve()
        {            
            this.OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;            
            this.CurveTypeId = 5;
            _presenterExport.GetCurveData();
            SessionManager.Instance.CurveExportView = this.CurveData.CurveExportView;
            SessionManager.Instance.CurveDeltaRefView = this.CurveData.CurveDeltaRefView;
            SessionManager.Instance.CurveDeltaPrevRunView = this.CurveData.CurveDeltaPrevRunView;
            rgCurveExportView.Rebind();
            rgCurveDeltaView.Rebind();            
        }

        protected void rgCurveExportView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (SessionManager.Instance.CurveExportView != null && SessionManager.Instance.CurveExportView != null && SessionManager.Instance.CurveExportView.Count > 0)
            {
                rgCurveExportView.DataSource = SessionManager.Instance.CurveExportView;     
            }
        }

        protected void rgCurveDeltaView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (SessionManager.Instance.CurveDeltaRefView != null && SessionManager.Instance.CurveDeltaRefView != null && SessionManager.Instance.CurveDeltaRefView.Count > 0)
            {
                rgCurveDeltaView.DataSource = SessionManager.Instance.CurveExportView;
            }
        }
        
        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short CurveTypeId { get; set; }        
        public Business.Messages.CurveDataResponse CurveData { get; set; }
        #endregion        
    

        
    }
}