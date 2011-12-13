using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurement : System.Web.UI.UserControl, IViewMeasurement
    {
        #region Declaration
        private PresenterMeasurement _presentermeasurement;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            _presentermeasurement = new PresenterMeasurement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                hdProjectID.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdSyncRunID.Value = SessionManager.Instance.CurrentSyncRun.SyncRunId.ToString();
                hdMeasurementTypeID.Value = SessionManager.Instance.CurrentProject.ReferenceChartTypeId.ToString();
                BindMeasurement();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //btnAnalyze.Enabled = SessionManager.Instance.CurrentSyncRun.HasMeasurements;
        }

        protected void btnBindGrid_Click(object sender, EventArgs e)
        {
            BindMeasurement();
        }

        private void BindMeasurement()
        {
            this.SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
            _presentermeasurement.GetMeasurements();
            SessionManager.Instance.CurrentSyncRun.HasMeasurements = (this.Measurements.Count > 0);
            //hdnHasMeasurement.Value = (!SessionManager.Instance.CurrentSyncRun.HasMeasurements).ToString().ToLower();
            //ScriptManager.RegisterClientScriptBlock(
            //    this,
            //    this.GetType(),
            //    "test",
            //    string.Format("ToggleAnalyze({0})", SessionManager.Instance.CurrentSyncRun.HasMeasurements.ToString().ToLower()),
            //    true);
            lvMeasurement.DataSource = this.Measurements;
            lvMeasurement.DataBind();
        }
        protected void lvMeasurement_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProject")
            {
                this.MeasurementId = Convert.ToInt32(e.CommandArgument);
                _presentermeasurement.DeleteMeasurement();
                BindMeasurement();
            }
        }
        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            Response.Redirect("SyncRunAnalyze.aspx");
        }

        #region IViewMeasurement Mebers
        public int SyncRunId { get; set; }
        public List<Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public int MeasurementDeleted { get; set; }
        #endregion
    }
}
