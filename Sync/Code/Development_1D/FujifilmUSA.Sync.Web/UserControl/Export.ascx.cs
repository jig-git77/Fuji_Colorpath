using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

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
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) //----Sync1D Case
                {
                    trExportViewForSync1D.Visible = true;
                    trAdvancedDownload.Visible = false;
                    DdlCurveType.SelectedValue = SessionManager.Instance.CurrentProject.CurveTypeID.ToString();
                    BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
                }
                else
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" project type, then skip Measure button and Advanced Download button (since we don't have desctination profile in this case).
                    {
                        trAdvancedDownload.Visible = false;
                        lbtnMeasure.Visible = false;
                        Div_Chart.Visible = false;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + ".icc");
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If "Spot Target Library" project type, then skip Measure button and DLP Download button (since we don't have desctination profile in this case).
                    {
                        trSourceProfDownload.Visible = false;
                        trDLPDownload.Visible = true;
                        trDLPDownload1.Visible = false;
                        trDLPDownload2.Visible = false;
                        trAdvancedDownload.Visible = true;
                        Div_Chart.Visible = true;
                        lbtnMeasure.Visible = true;
                        locDownloadDLPAndChart.Text = "Download Chart";
                        locDownloadDestinationICC.Text = "Download Named Color Profile ICC";
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + ".icc");
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //---If "Custom Source Profile" project type, then skip Measure button and Advanced Download button (since we don't have desctination profile in this case).
                    {
                        trSourceProfDownload.Visible = true;
                        trDLPDownload.Visible = false;
                        trAdvancedDownload.Visible = false;
                        lbtnMeasure.Visible = false;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + ".icc");
                    }
                    else  //----Sync4D or Ink Optimization case
                    {
                        trAdvancedDownload.Visible = true;
                    }
                }
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1)
                {
                    rbtnDetlaReflist.Items[1].Enabled = false;
                }
            }
        }

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                _presenterExport.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterExport.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        Response.Redirect("Spot_Measurement.aspx");
                    }
                }
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                CurveTypeId = Convert.ToInt16(HD_CurveType.Value);
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
                DLPName = SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc";
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" / "Custom Source Profile" project type, then only Project name as DLP name.
                {
                    DLPName = SessionManager.Instance.CurrentProject.ProjectName + ".icc";
                }
            }
            if (!IsErrorOccured)
            {
                if (DeviceLinkProfile != null)
                    Utilities.DownloadFile(DeviceLinkProfile, string.Format("{0}", DLPName));
            }
        }

        protected void lbtnDownloadSourceProf_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetSourceProfile();
            if (!IsErrorOccured)
            {
                if (SourceProfile != null)
                    Utilities.DownloadFile(SourceProfile,
                                           string.Format("{0}.icc",
                                                         SessionManager.Instance.CurrentProject.ProjectName));
            }
        }


        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/"), "spotchart.zip", "spotchart.zip");
            }
            else
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
        }

        protected void lbtnDownloadDestinationICC_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                OptimizeRunId = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().OptimizeRunId;
                _presenterExport.GetNamedColorProfile();
                if (!IsErrorOccured)
                {
                    if (SourceProfile != null)
                        Utilities.DownloadFile(SourceProfile,
                                               string.Format("{0}.icc",
                                                             SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName));
                }

                Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/"), "Named Color Profile.icc", "Named Color Profile.icc");

            }
            else
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
        }

        private void BindCurve(short _CurveTypeID)
        {
            this.OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            if (_CurveTypeID == 100 || _CurveTypeID == 101) //---Set same value for "ColorGate-TVI" and "ColorGate-CGCX" like "5%" for export curve data
            {
                _CurveTypeID = 5;
            }
            if (_CurveTypeID == 102) //---Set same value for "Onyx G7 Import" like "Nexus" for export curve data
            {
                _CurveTypeID = 45;
            }
            this.CurveTypeId = _CurveTypeID;
            _presenterExport.GetCurveData();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurveExportView = this.CurveData.CurveExportView;
                SessionManager.Instance.CurveDeltaRefView = this.CurveData.CurveDeltaRefView;
                SessionManager.Instance.CurveDeltaPrevRunView = this.CurveData.CurveDeltaPrevRunView;
                BindCharts(this.CurveData.CurveExportView, this.CurveData.CurveDeltaRefView, this.CurveData.CurveDeltaPrevRunView);
                rgCurveExportView.Rebind();
                rgCurveDeltaView.Rebind();
            }
        }

        #region Bind Charts
        private void BindCharts(List<Entity.CPSync1D.OptimizeRunCurve> CurveExportView, List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaRefView, List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaPrevRunView)
        {
            BindCurveChart(CurveExportView);
            BindDeltaChart(CurveDeltaRefView, CurveDeltaPrevRunView);
        }

        private void BindCurveChart(List<Entity.CPSync1D.OptimizeRunCurve> CurveExportView)
        {
            for (int i = 0; i < CurveExportView.Count; i++)
            {
                if (Chk_C.Checked == true)
                    AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series1"], CurveExportView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveExportView[i].CMYK_C).ToString(), Color.Cyan.Name.ToString(), true);
                if (Chk_M.Checked == true)
                    AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series2"], CurveExportView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveExportView[i].CMYK_M).ToString(), Color.Magenta.Name.ToString(), true);
                if (Chk_Y.Checked == true)
                    AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series3"], CurveExportView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveExportView[i].CMYK_Y).ToString(), Color.Yellow.Name.ToString(), true);
                if (Chk_K.Checked == true)
                    AddPointChartWithColor(i, CHRT_ExportCurve.Series["Series4"], CurveExportView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveExportView[i].CMYK_K).ToString(), Color.Black.Name.ToString(), true);
            }
        }

        private void BindDeltaChart(List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaRefView, List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaPrevRunView)
        {
            Int64 MaxDelta = 0;
            Int64 MinDelta = 0;

            List<Entity.CPSync1D.OptimizeRunCurve> CurveDeltaView = new List<Entity.CPSync1D.OptimizeRunCurve>();

            if (rbtnDetlaReflist.Items[0].Selected == true)
            {
                CurveDeltaView = CurveDeltaRefView; //-----Current Run's delta view
            }
            else
            {
                CurveDeltaView = CurveDeltaPrevRunView; //-----Previous Run's delta view
            }

            if (CurveDeltaView != null && CurveDeltaView.Count > 0)
            {
                #region Fine Max Min
                if (Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_C)) > MaxDelta)
                    MaxDelta = Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_C));
                if (Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_M)) > MaxDelta)
                    MaxDelta = Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_M));
                if (Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_Y)) > MaxDelta)
                    MaxDelta = Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_Y));
                if (Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_K)) > MaxDelta)
                    MaxDelta = Convert.ToInt64(CurveDeltaView.Max(x => x.CMYK_K));

                if (Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_C)) < MinDelta)
                    MinDelta = Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_C));
                if (Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_M)) < MinDelta)
                    MinDelta = Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_M));
                if (Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_Y)) < MinDelta)
                    MinDelta = Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_Y));
                if (Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_K)) < MinDelta)
                    MinDelta = Convert.ToInt64(CurveDeltaView.Min(x => x.CMYK_K));


                MinDelta = (MinDelta * -1);

                MaxDelta = MaxDelta + 1;
                MinDelta = MinDelta + 1;

                if (MaxDelta > MinDelta)
                {
                    MinDelta = (MaxDelta * -1);
                }
                else
                {
                    MaxDelta = MinDelta;
                    MinDelta = (MinDelta * -1);
                }
                CHRT_ExportCurveDeltaRef.ChartAreas["Default"].AxisY.Minimum = MinDelta;
                CHRT_ExportCurveDeltaRef.ChartAreas["Default"].AxisY.Maximum = MaxDelta;
                #endregion

                #region BindChart
                for (int i = 0; i < CurveDeltaView.Count; i++)
                {
                    if (Chk_C.Checked == true)
                        AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series1"], CurveDeltaView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveDeltaView[i].CMYK_C).ToString(), Color.Cyan.Name.ToString(), false);
                    if (Chk_M.Checked == true)
                        AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series2"], CurveDeltaView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveDeltaView[i].CMYK_M).ToString(), Color.Magenta.Name.ToString(), false);
                    if (Chk_Y.Checked == true)
                        AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series3"], CurveDeltaView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveDeltaView[i].CMYK_Y).ToString(), Color.Yellow.Name.ToString(), false);
                    if (Chk_K.Checked == true)
                        AddPointChartWithColor(i, CHRT_ExportCurveDeltaRef.Series["Series4"], CurveDeltaView[i].ReferenceNo.ToString(), Convert.ToInt64(CurveDeltaView[i].CMYK_K).ToString(), Color.Black.Name.ToString(), false);
                }
                #endregion
            }
        }

        #endregion

        protected void rgCurveExportView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (SessionManager.Instance.CurveExportView != null && SessionManager.Instance.CurveExportView != null && SessionManager.Instance.CurveExportView.Count > 0)
            {
                rgCurveExportView.DataSource = SessionManager.Instance.CurveExportView;
            }
        }

        protected void rgCurveDeltaView_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (rbtnDetlaReflist.SelectedIndex == 0)
            {
                if (SessionManager.Instance.CurveDeltaRefView != null && SessionManager.Instance.CurveDeltaRefView != null && SessionManager.Instance.CurveDeltaRefView.Count > 0)
                {
                    rgCurveDeltaView.DataSource = SessionManager.Instance.CurveDeltaRefView;
                }
            }
            else
            {
                if (SessionManager.Instance.CurveDeltaPrevRunView != null && SessionManager.Instance.CurveDeltaPrevRunView != null && SessionManager.Instance.CurveDeltaPrevRunView.Count > 0)
                {
                    rgCurveDeltaView.DataSource = SessionManager.Instance.CurveDeltaPrevRunView;
                }
            }
        }

        protected void BtnExportCurvesExportView_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.Instance.CurveExportView == null)
                {
                    BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));  //-----Rebind session if session is empty.
                }
                if (SessionManager.Instance.CurveExportView != null)
                {
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                    //==========  Get Data Table and Bind ==========//
                    List<Entity.CPSync1D.OptimizeRunCurve> DT_CPSyncPressRunCurve = new List<Entity.CPSync1D.OptimizeRunCurve>();
                    DT_CPSyncPressRunCurve = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveExportView;

                    string CurveFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00");

                    if (DdlCurveType.SelectedValue == "1" || DdlCurveType.SelectedValue == "5" || DdlCurveType.SelectedValue == "10")
                    {
                        #region Create file and download txt file for 1%,5% and 10%
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurvesFile.txt");
                        //const string quote = "\"";
                        sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                        sw.WriteLine("BEGIN_DATA_FORMAT");
                        sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        //sw.WriteLine("SampleID\tSampleName\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        sw.WriteLine("END_DATA_FORMAT");
                        sw.WriteLine("NUMBER_OF_SETS\t" + DT_CPSyncPressRunCurve.Count);
                        sw.WriteLine("BEGIN_DATA");
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            sw.WriteLine(DT_CPSyncPressRunCurve[i].ReferenceNo.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_C.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_M.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_Y.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_K.ToString());
                        }
                        sw.WriteLine("END_DATA");
                        sw.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.txt", CurveFileName + "_CurvesFile.txt");
                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "15")  // Rampage
                    {
                        #region Create Zip file and download file for rampage
                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_C.oli");
                        swC.WriteLine("2");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("25");

                        swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_M.oli");
                        swM.WriteLine("2");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("25");

                        swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Y.oli");
                        swY.WriteLine("2");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("25");

                        swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_K.oli");
                        swK.WriteLine("2");
                        swK.WriteLine("0.0000000000");
                        swK.WriteLine("0.0000000000");
                        swK.WriteLine("25");

                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_C).ToString("0.0"));
                            swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_M).ToString("0.0"));
                            swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_Y).ToString("0.0"));
                            swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_K).ToString("0.0"));
                        }
                        swC.WriteLine("Version: 2.0");
                        swC.Close();

                        swM.WriteLine("Version: 2.0");
                        swM.Close();

                        swY.WriteLine("Version: 2.0");
                        swY.Close();

                        swK.WriteLine("Version: 2.0");
                        swK.Close();

                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_C.oli");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_M.oli");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Y.oli");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_K.oli");

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CMYKFile.zip", CurveFileName + "_CMYKFile.zip");
                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "25")  // for XMF
                    {
                        #region Create file and download txt file for XMF
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurvesFile.csv");
                        sw.WriteLine("Cyan;Magenta;Yellow;Black");
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            sw.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_C).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_M).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_Y).ToString("0.0000") + ";" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_K).ToString("0.0000"));
                        }
                        sw.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.csv", CurveFileName + "_CurvesFile.csv");
                        #endregion

                    }
                    else if (DdlCurveType.SelectedValue == "35")  // Harmony (Prinergy)
                    {
                        #region Create Zip file and download file for Harmony
                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;

                        //cyan
                        swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_C.txt");

                        //magenta
                        swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_M.txt");

                        //yellow    
                        swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Y.txt");

                        //black
                        swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_K.txt");

                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_C).ToString("0.00"));
                            swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_M).ToString("0.00"));
                            swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_Y).ToString("0.00"));
                            swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.00") + "\t" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_K).ToString("0.00"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_C.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_M.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Y.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Harmony.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_K.txt");

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Harmony.zip", CurveFileName + "_Harmony.zip");
                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "45")  // nexus
                    {
                        #region Create Zip file and download file for nexus

                        //check if file exists and delete it
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip") == true)
                            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip");

                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Cya.txt");
                        swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Mag.txt");
                        swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Yel.txt");
                        swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Bla.txt");


                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            swC.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_C).ToString("0.0000"));
                            swM.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_M).ToString("0.0000"));
                            swY.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_Y).ToString("0.0000"));
                            swK.WriteLine(Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0000") + "    " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_K).ToString("0.0000"));
                        }

                        swC.Close();
                        swM.Close();
                        swY.Close();
                        swK.Close();

                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Cya.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Mag.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Yel.txt");
                        ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_Nexus_Bla.txt");

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_Nexus.zip", CurveFileName + "_Nexus.zip");
                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "55")  // for sericol UK Caldera
                    {
                        #region Create file and download txt file for sericol UK
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurvesFile.ppc");
                        sw.WriteLine("Device = CMYK");
                        sw.WriteLine("Max ink = 400");
                        sw.WriteLine("Max ink reduction = preserve black");

                        string strCyan = "";
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {

                            strCyan = strCyan + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_C).ToString("0.0") + ") , ";

                        }
                        strCyan = strCyan.Remove(strCyan.Length - 3, 3);
                        sw.WriteLine("Cyan curve = " + strCyan);

                        string strMagenta = "";
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {

                            strMagenta = strMagenta + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_M).ToString("0.0") + ") , ";

                        }
                        strMagenta = strMagenta.Remove(strMagenta.Length - 3, 3);
                        sw.WriteLine("Magenta curve = " + strMagenta);

                        string strYellow = "";
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {

                            strYellow = strYellow + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_Y).ToString("0.0") + ") , ";

                        }
                        strYellow = strYellow.Remove(strYellow.Length - 3, 3);
                        sw.WriteLine("Yellow curve = " + strYellow);

                        string strBlack = "";
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {

                            strBlack = strBlack + "(" + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].ReferenceNo).ToString("0.0") + " " + Convert.ToDecimal(DT_CPSyncPressRunCurve[i].CMYK_K).ToString("0.0") + ") , ";

                        }
                        strBlack = strBlack.Remove(strBlack.Length - 3, 3);
                        sw.WriteLine("Black curve = " + strBlack);


                        sw.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.ppc", CurveFileName + "_CurvesFile.ppc");
                        #endregion

                    }
                    else if (DdlCurveType.SelectedValue == "100")  // for ColorGate-TVI
                    {
                        #region Create file and download .ctvx file for ColorGate-TVI

                        //-------For curvetype=100-------Make .ctvx XML file for ColorGate-TVI (Dhruti's Code)-------------------//

                        //-----------------Pass few parameters in "" marks to .ctvx xml file------------------//
                        string str0 = "\"" + "1.0" + "\"";
                        string str1 = "\"" + "UTF-8" + "\"";
                        string str2 = "\"" + "urn:schemas-colorgate-com:tonalvalues" + "\"";
                        string str3 = "\"" + "1" + "\"";

                        //-----------------Make .ctvx xml file------------------//
                        System.IO.StreamWriter sw3;
                        sw3 = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurveDelta.ctvx");
                        sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                        sw3.WriteLine("<TonalValues xmlns=" + str2 + ">");

                        if (DT_CPSyncPressRunCurve.Count > 19)
                        {
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_K.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_K.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_K.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_K.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_K.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_C.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_C.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_C.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_C.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_C.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_M.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_M.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_M.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_M.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_M.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_Y.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[2].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_Y.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[5].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_Y.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[10].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_Y.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[15].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + (Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_Y.ToString()) - Convert.ToDecimal(DT_CPSyncPressRunCurve[18].ReferenceNo.ToString())) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>0</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        }
                        sw3.WriteLine("</TonalValues>");
                        sw3.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.ctvx", CurveFileName + "_CurveDelta.ctvx");

                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "101")  // for ColorGate-CGCX
                    {
                        #region Create file and download .cgcx file for ColorGate-CGCX

                        //-------For curvetype=101-------Make .cgcx XML file for ColorGate-CGCX (Dhruti's Code)-------------------//

                        //-----------------Pass few parameters in "" marks to .cgcx xml file------------------//
                        string str0 = "\"" + "1.0" + "\"";
                        string str1 = "\"" + "UTF-8" + "\"";
                        string str2 = "\"" + "urn:schemas-colorgate-com:gradation" + "\"";
                        string str3 = "\"" + "1" + "\"";

                        //-----------------Make .cgcx xml file------------------//
                        System.IO.StreamWriter sw3;
                        sw3 = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurveDelta.cgcx");
                        sw3.WriteLine("<?xml version=" + str0 + " encoding=" + str1 + "?>");
                        sw3.WriteLine("<GradationCurves xmlns=" + str2 + ">");

                        if (DT_CPSyncPressRunCurve.Count > 19)
                        {
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>0</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_K.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>1</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_C.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>2</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_M.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>3</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.1</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[2].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0</X><Y>1.224646799e-017</Y></Token><Token><X>0.2</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.25</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[5].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.15</X><Y>1.224646799e-017</Y></Token><Token><X>0.35</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.5</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[10].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.4</X><Y>1.224646799e-017</Y></Token><Token><X>0.6</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.75</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[15].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.65</X><Y>1.224646799e-017</Y></Token><Token><X>0.85</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>3</Code><Tokens><Token><X>0.9</X><Y>" + Convert.ToDecimal(DT_CPSyncPressRunCurve[18].CMYK_Y.ToString()) / 100 + "</Y></Token><Token><X>0.8</X><Y>1.224646799e-017</Y></Token><Token><X>1</X><Y>-1.224646799e-017</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                            sw3.WriteLine("<ColorTansfer version=" + str3 + "><ChannelID>4</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>5</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>6</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>7</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer><ColorTansfer version=" + str3 + "><ChannelID>8</ChannelID><ColorTansferPoint><Code>1</Code><Tokens><Token><X>0</X><Y>0</Y></Token></Tokens></ColorTansferPoint><ColorTansferPoint><Code>2</Code><Tokens><Token><X>1</X><Y>1</Y></Token></Tokens></ColorTansferPoint></ColorTansfer>");
                        }
                        sw3.WriteLine("</GradationCurves>");
                        sw3.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.cgcx", CurveFileName + "_CurveDelta.cgcx");

                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "102")  // for Onyx G7 Import
                    {
                        #region Create file and download txt file for weighted highlight shadow and then 5% increment
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurvesFile.txt");
                        sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                        sw.WriteLine("BEGIN_DATA_FORMAT");
                        sw.WriteLine("SampleID\tSampleName\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                        sw.WriteLine("END_DATA_FORMAT");
                        sw.WriteLine("NUMBER_OF_SETS\t" + DT_CPSyncPressRunCurve.Count.ToString());
                        sw.WriteLine("BEGIN_DATA");
                        for (int i = 0; i < DT_CPSyncPressRunCurve.Count; i++)
                        {
                            sw.WriteLine("A" + i.ToString() + "\t\"" + DT_CPSyncPressRunCurve[i].ReferenceNo.ToString() + ".00" + "\"\t" + DT_CPSyncPressRunCurve[i].CMYK_C.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_M.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_Y.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_K.ToString());
                        }
                        sw.WriteLine("END_DATA");
                        sw.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurvesFile.txt", CurveFileName + "_CurvesFile.txt");
                        #endregion
                    }
                }
            }
            catch (Exception Exc)
            {

            }
        }



        protected void DdlCurveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
            HD_CurveType.Value = DdlCurveType.SelectedValue;
        }

        protected void Chk_CheckedChanged(object sender, EventArgs e)
        {
            BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
        }

        protected void rbtnDetlaReflist_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
        }

        protected void BtnExportCurvesDeltaView_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.Instance.CurveDeltaRefView == null || SessionManager.Instance.CurveDeltaPrevRunView == null)
                {
                    BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue)); //-----Rebind session if session is empty.
                }

                List<Entity.CPSync1D.OptimizeRunCurve> DT_CurveDelta = new List<Entity.CPSync1D.OptimizeRunCurve>();
                if (rbtnDetlaReflist.Items[0].Enabled == true && rbtnDetlaReflist.Items[0].Selected == true)
                {
                    DT_CurveDelta = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveDeltaRefView;
                }
                else { DT_CurveDelta = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveDeltaPrevRunView; }

                if (DT_CurveDelta != null)
                {
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                    string CurveFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00");

                    #region Create file and download txt file for all curve types
                    //---------------For all Curve Types -------------------//
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurveDelta.txt");
                    sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                    sw.WriteLine("BEGIN_DATA_FORMAT");
                    sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS\t" + DT_CurveDelta.Count);
                    sw.WriteLine("BEGIN_DATA");
                    for (int i = 0; i < DT_CurveDelta.Count; i++)
                    {
                        sw.WriteLine(DT_CurveDelta[i].ReferenceNo.ToString() + "\t" + DT_CurveDelta[i].CMYK_C.ToString() + "\t" + DT_CurveDelta[i].CMYK_M.ToString() + "\t" + DT_CurveDelta[i].CMYK_Y.ToString() + "\t" + DT_CurveDelta[i].CMYK_K.ToString());
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();

                    Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.txt", CurveFileName + "_CurveDelta.txt");
                    #endregion
                }
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }


        //Add Point with color on charts
        private void AddPointChartWithColor(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, bool ShowMarker)
        {
            try
            {
                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Common.Utilities.DecimalRound(Convert.ToDouble(_XValue)), Common.Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                if (ShowMarker)
                {
                    _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
                    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                    _objSeries.Points[_Index].MarkerSize = 2;
                    _objSeries.Points[_Index].BorderWidth = 1;
                    _objSeries.Points[_Index].MarkerBorderColor = ColorTranslator.FromHtml("#000000");
                }
                else
                {
                    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
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
        public int SpotProjectId { get; set; }
        #endregion


    }
}