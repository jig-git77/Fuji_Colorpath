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

        //protected void grdRecentUploadList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    dynamic data = new[] {
        //        new { MeasurementName = "123456", UploadDateTime = DateTime.Now},
        //         new { MeasurementName = "654231", UploadDateTime = DateTime.Now.AddMinutes(10)}
                
        //    };
        //    //  grdRecentUploadList.DataSource = data;
        //}
        protected void btnBindGrid_Click(object sender, EventArgs e)
        {
            BindMeasurement();
        }

        private void BindMeasurement()
        {
            //if (SessionManager.Instance.CurrentSyncRun.SyncRunId != null)
            //{
                this.SyncRunId = SessionManager.Instance.CurrentSyncRun.SyncRunId;
                _presentermeasurement.GetMeasurements();

                lvMeasurement.DataSource = this.Measurements;
                lvMeasurement.DataBind();
               
            //}
        }
        #region IViewMeasurement Mebers
        public int SyncRunId { get; set; }
        public List<Measurement> Measurements { get; set; }
        #endregion

        protected void lvMeasurement_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //if (e.CommandName == "DeleteProject")
            //{
            //    this.SyncRunId =Convert.ToInt32(e.CommandArgument);
            //    _presentermeasurement.DeleteMeasurements
            //}
        }

        
    }
}
