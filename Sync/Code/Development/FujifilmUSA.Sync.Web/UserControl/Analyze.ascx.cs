using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : BaseControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        public DateTime EndDate;
        public string AddMeasurementDeleteMeasurementConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public bool IsNewDlp
        {
            get { return SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1; }
        }

        public bool IsDlpCreated
        {
            get { return SessionManager.Instance.CurrentOptimizeRun.HasProfile; }
        }

        public string CreateDLPUpdateProgressClientID
        {
            get { return updateProgress.ClientID; }
        }
        public string UpdateProgressClientID
        {
            get { return ((UpdateProgress)Page.Master.FindControl("UpdateProgress")).ClientID; }
        }

        public string DLPMessage
        {
            get
            {
                string DLPName = SessionManager.Instance.CurrentProject.ProjectName + " DLP" + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + ".icc";
                return string.Format("Once optimization is complete, apply {0} to workflow and reprint the {1} chart.", DLPName, SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value);
            }
        }
        #endregion

        #region Page Method
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
            }

            if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
            {
                header.InnerText = "New Print Condition - Analyze (No DLP)";
            }
            else
            {
                header.InnerText = string.Format("Analyze - {0}", SessionManager.Instance.CurrentOptimizeRun.LastDlpName);
            }
            
            lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                              ? "<span><img id='imgAdd' src='Images/export.png'/>Create DLP<span>"
                                              : "<span><img id='imgAdd' src='Images/save_icon.png'/>Next<span>";
            if (!Page.IsPostBack)
            {
                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Closed && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                {
                    //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                    lbtnDownloadAndSaveDLP.Visible = false;
                }
                BindMesurementChart();
                ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;
                BindAvgDelta();
            }
            BindMesurementChart();
        }
        #endregion

        #region Methods        

        private void BindMesurementChart()
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            Measurements = new List<Entity.Measurement>();
            _presenterAnalyze.GetAnalyzeChartMeasurement();
            if (!IsErrorOccured)
            {
                var manager = new AnalyzeChartManager(chartAnalyze, Measurements)
                {
                    CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value)
                };
                manager.BindChart();

                lblMeasurementName.Text = manager.MeasurementName;

                hdMeasurement.Value = manager.CurrentMeasurementID.ToString();

                //----------Bind Patch Details Gridview depending on selected measurement from Analyze Chart on "Patch Details" Tab-------//
                GridPatchDetails.Rebind();
            }
        }

        private void BindAvgDelta()    //------Code for Avg Delta display on "Summary" Tab
        {
            try
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                MeasurementsDelta = new List<Entity.MeasurementPatch_AvgDelta>();
                _presenterAnalyze.GetAvgDeltaMeasurement();
                grdMeasurement.DataSource = MeasurementsDelta;
                grdMeasurement.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        public void ConfigureExport()
        {
            GridPatchDetails.ExportSettings.ExportOnlyData = true;
            GridPatchDetails.ExportSettings.IgnorePaging = true;
            GridPatchDetails.ExportSettings.OpenInNewWindow = true;

            GridPatchDetails.MasterTableView.Columns.FindByUniqueName("HtmlColor").Visible = false;
        }

        #endregion

        #region Control Events

        protected void hdChartButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hdCheckBoxChange.Value) == 1)
            {
                Measurement = new Entity.Measurement();
                Measurement.MeasurementId = Convert.ToInt32(hdMeasurement.Value);
                Measurement.IsUsed = hdChartCheckBox.Checked;

                _presenterAnalyze.UpdateMeasurementByAnalyzeChart();
            }
            if (!IsErrorOccured)
            {
                BindMesurementChart();
            }
        }

        protected void lbtnDownloadAndSaveDLP_Click(object sender, EventArgs e)
        {
            if (!IsNewDlp && IsDlpCreated)
            {
                Response.Redirect("Export.aspx");
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    DestinationProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
                    if (string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.CurrentDlpName))
                    {
                        DlpDescription = string.Format("{0}.icc", string.Format("{0} DLP 000", SessionManager.Instance.CurrentProject.ProjectName));
                    }
                    else
                    {
                        DlpDescription = string.Format("{0}", SessionManager.Instance.CurrentProject.CurrentDlpName);
                    }

                    EndDate = System.DateTime.Now;
                    _presenterAnalyze.CreateDestinationAndDlp();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject.OptimizeRuns[0].EndDateTime = EndDate;
                        SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile = true;
                        Response.Redirect("Export.aspx");
                    }
                }
            }
        }

        protected void GridPatchDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                MeasurementId = 0;
                if (hdMeasurement.Value != "")
                {
                    MeasurementId = Convert.ToInt32(hdMeasurement.Value);  //----Pass selected measurement id from analyze chart above

                    OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                    PatchDetails = new List<Entity.MeasurementPatchDetails>();
                    _presenterAnalyze.GetPatchDetailsofMeasurement();
                    GridPatchDetails.DataSource = PatchDetails;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridPatchDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                Label lbl_HTML = (Label)item["HtmlColor"].FindControl("lbl_HTML");
                lbl_HTML.BackColor = System.Drawing.ColorTranslator.FromHtml(item.GetDataKeyValue("HtmlColor").ToString());
            }
        }

        protected void btn_Excel_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            GridPatchDetails.MasterTableView.ExportToExcel();
        }

        protected void btn_CSV_Click(object sender, EventArgs e)
        {
            ConfigureExport();
            GridPatchDetails.MasterTableView.ExportToCSV();
        }

        #endregion

        #region IViewAnalyze Members

        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }

        public List<int> MeasurementIds { get; set; }

        public List<Entity.Measurement> Measurements { get; set; }

        public Entity.Measurement Measurement { get; set; }

        public string DestinationProfileDescription { get; set; }

        public string DlpDescription { get; set; }

        public List<MeasurementPatch_AvgDelta> MeasurementsDelta { get; set; }

        public List<MeasurementPatchDetails> PatchDetails { get; set; }

        public int MeasurementId { get; set; }

        #endregion

        protected void ImgBtn_Report_Click(object sender, ImageClickEventArgs e)
        {

            byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/PDFReports/Sync DLP Report.pdf"));
            Utilities.DownloadFile(bytes, SessionManager.Instance.CurrentProject.ProjectName + " Report", ".pdf");
        }

    }
}
