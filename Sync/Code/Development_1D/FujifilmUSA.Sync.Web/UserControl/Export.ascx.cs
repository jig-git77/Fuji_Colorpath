using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.IO;
using System.Data;
using System.Collections.Generic;

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
                    BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
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

        private void BindCurve(short _CurveTypeID)
        {            
            this.OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;            
            this.CurveTypeId = _CurveTypeID;
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

        protected void BtnExportCurvesExportView_Click(object sender, EventArgs e)
        {
            try
            {
                BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
                if (SessionManager.Instance.CurveExportView != null)
                {
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                    //==========  Get Data Table and Bind ==========//
                    List<Entity.CPSync1D.OptimizeRunCurve> DT_CPSyncPressRunCurve = new List<Entity.CPSync1D.OptimizeRunCurve>();
                    DT_CPSyncPressRunCurve = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveExportView;

                    if (DdlCurveType.SelectedValue == "1" || DdlCurveType.SelectedValue == "5" || DdlCurveType.SelectedValue == "10")
                    {
                        #region Create file and download txt file for 1%,5% and 10%
                        System.IO.StreamWriter sw;
                        sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_CurvesFile.txt");
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
                            sw.WriteLine(DT_CPSyncPressRunCurve[i].ReferenceNo.ToString()  + "\t" + DT_CPSyncPressRunCurve[i].CMYK_C.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_M.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_Y.ToString() + "\t" + DT_CPSyncPressRunCurve[i].CMYK_K.ToString());
                        }
                        sw.WriteLine("END_DATA");
                        sw.Close();

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), SessionManager.Instance.CurrentUser.UserId + "_CurvesFile.txt", SessionManager.Instance.CurrentUser.UserId + "_CurvesFile.txt");
                        #endregion
                    }
                    else if (DdlCurveType.SelectedValue == "15")  // Rampage
                    {
                        #region Create Zip file and download file for rampage
                        System.IO.StreamWriter swC;
                        System.IO.StreamWriter swM;
                        System.IO.StreamWriter swY;
                        System.IO.StreamWriter swK;
                        swC = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_C.oli");
                        swC.WriteLine("2");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("0.0000000000");
                        swC.WriteLine("25");

                        swM = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_M.oli");
                        swM.WriteLine("2");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("0.0000000000");
                        swM.WriteLine("25");

                        swY = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_Y.oli");
                        swY.WriteLine("2");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("0.0000000000");
                        swY.WriteLine("25");

                        swK = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_K.oli");
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

                        //Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_C.oli");
                        //Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_M.oli");
                        //Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_Y.oli");
                        //Fuji.Common.ZipManager.AddFileToZip(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + SessionManager.Instance.CurrentUser.UserId + "_K.oli");

                        Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip", SessionManager.Instance.CurrentUser.UserId + "_CMYKFile.zip");
                        #endregion
                    }
                }
            }
            catch (Exception Exc)
            {

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

        protected void DdlCurveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
            }
            catch (Exception Exc)
            {
                
            }
        }

        protected void Chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindCurve(Convert.ToInt16(DdlCurveType.SelectedValue));
               // UpdPaDeltaView.Update();
            }
            catch (Exception Exc)
            {
                
            }
        }
    

        
    }
}