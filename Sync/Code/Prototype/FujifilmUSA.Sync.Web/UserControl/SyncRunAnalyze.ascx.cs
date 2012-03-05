using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SyncRunAnalyze : System.Web.UI.UserControl, IViewSyncRunAnalyze
    {
        #region Variable Declarations
        private PresenterSyncRunAnalyze _presenterSyncRunAnalyze;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        #endregion


        #region Page Method
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenterSyncRunAnalyze = new PresenterSyncRunAnalyze(this);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnOptimize.Visible = btnExport.Visible = btnCertify.Visible = false;
            switch ((ECPSyncRunStatus)Enum.Parse(typeof(ECPSyncRunStatus), SessionManager.Instance.CurrentSyncRun.Status.ToString()))
            {
                case ECPSyncRunStatus.Open:
                    btnOptimize.Visible = !SessionManager.Instance.CurrentSyncRun.HasProfile;
                    btnExport.Visible = SessionManager.Instance.CurrentSyncRun.HasProfile;
                    btnCertify.Visible = true;
                    break;
                case ECPSyncRunStatus.Closed:
                    btnExport.Visible = true;
                    break;
                case ECPSyncRunStatus.Certify:
                    break;
                case ECPSyncRunStatus.NotUsed:
                    break;
            }
            BindMeasurementList();
        }
        #endregion

        #region Methods

        public void BindMeasurementList()
        {
            lstMeasurement = new System.Collections.Generic.List<Measurement>();
            if (SessionManager.Instance.CurrentSyncRun != null)
            {
                SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
                _presenterSyncRunAnalyze.GetMeasurementsBySyncId();
            }
            grdMeasurement.DataSource = lstMeasurement;
            grdMeasurement.DataBind();
            
        }

        #endregion

        #region Control Events
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Redirect("SyncRunExport.aspx");
        }

        protected void btnOptimize_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            ProfileOptionId = SessionManager.Instance.CurrentProject.ProfileOptionId;
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            MeasurementIds = string.Empty; //TODO: Add Measurement Ids
            _presenterSyncRunAnalyze.OptimizeProfile();
            SessionManager.Instance.CurrentSyncRun.HasProfile = true;
            
            Response.Redirect("SyncRunExport.aspx");
        }

        protected void btnCertify_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            Status = ECPSyncRunStatus.Certify;
            EndDateTime = DateTime.UtcNow;
            _presenterSyncRunAnalyze.CertifySyncRunProcess();
            SessionManager.Instance.CurrentProject = null;
            SessionManager.Instance.CurrentSyncRun = null;
            Response.Redirect("ProjectList.aspx");
        }

        protected void grdMeasurement_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            Measurement = new Measurement();
            Measurement.MeasurementId = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "ExportTxt":
                        _presenterSyncRunAnalyze.GetMeasurementPatch();
                        ExportFile<MeasurementPatch> txtConverter = new ExportFile<MeasurementPatch>(Measurement.Patches, "Export.txt");
                        txtConverter.ExecuteResult();
                    break;
                case "ExportCvs":
                        _presenterSyncRunAnalyze.GetMeasurementPatch();
                        ExportFile<MeasurementPatch> cvsConverter = new ExportFile<MeasurementPatch>(Measurement.Patches, "Export.csv");
                        cvsConverter.ExecuteResult();
                    break;
                case "DeleteMeasurement":
                        _presenterSyncRunAnalyze.DeleteMeasurement();
                        BindMeasurementList();
                    break;
            }
        }
        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
            ucStatusbar1.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewSyncRunAnalyze Members

        public int SyncRunId { get; set; }
        
        public int ProjectId{ get; set; }

        public int ProfileOptionId { get; set; }

        public string MeasurementIds{ get; set; }

        public System.Collections.Generic.List<Measurement> lstMeasurement{ get; set; }

        public Measurement Measurement { get; set;}

        #endregion

        #region IViewBaseSyncDevice Members

        public int SyncDeviceId{ get; set; }
        
        #endregion

        #region IViewSyncRunAnalyze Members

        public ECPSyncRunStatus Status { get; set; }
        
        public DateTime EndDateTime { get; set; }
        
        #endregion

    }


    

}
