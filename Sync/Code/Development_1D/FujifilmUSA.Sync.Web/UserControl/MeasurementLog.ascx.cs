using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSync1D;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementLog : BaseControl, IViewMeasurement
    {
        #region Declaration
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        #endregion

        #region Constructor
        public MeasurementLog()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!Page.IsPostBack)
            {
                BindMeasurementLogList();
            }
        }
        #endregion

        #region Methods

        public void BindMeasurementLogList()
        {
            _presenterMeasurementSetting.GetMeasurementErrorLogList();
            if (!IsErrorOccured)
            {
                rgMeasurementLog.DataSource = MeasurementLogs;
                rgMeasurementLog.DataBind();
            }
        }

        #endregion

        #region Control Events
        protected void rgMeasurementLog_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _presenterMeasurementSetting.GetMeasurementErrorLogList();
            if (!IsErrorOccured)
            {
                rgMeasurementLog.DataSource = MeasurementLogs;                
            }
        }
        protected void rgMeasurementLog_ItemCommand(object sender, GridCommandEventArgs e)
        {            
            switch (e.CommandName)
            {
                case "Filter":
                    break;                
            }
        }
        #endregion

        #region IViewMeasurement
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public string ProfileType { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        public List<Entity.MeasurementLog> MeasurementLogs { get; set; }
        public Project Project { get; set; }
        public List<int> MeasurementIds { get; set; }
        public int MeasurementId { get; set; }
        public bool EnabledPaperScaling { get; set; }
        public bool IsUpdateNewPaper { get; set; }
        public MeasurementPatchDetails measurementPatchDetails { get; set; }
        public bool IsUpdateDelta { get; set; }
        public int LicenseTypeId { get; set; }
        public List<ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public int ProjectTypeId { get; set; }
        public int PrintstandardTypeId { get; set; }
        #endregion
    }
}