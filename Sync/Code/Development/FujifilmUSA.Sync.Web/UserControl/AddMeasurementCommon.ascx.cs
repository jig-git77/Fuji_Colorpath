using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddMeasurementCommon : System.Web.UI.UserControl
    {
        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public int ProfileId { get { return Convert.ToInt32(hdProfileID.Value); } }

        public ProfileType Type { get; set; }

        #endregion

        #region Page Event

        protected override void OnInit(EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            if (!IsPostBack)
            {
                if (Type == ProfileType.Optimization)
                {
                    hdProjectID.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                    hdSyncRunID.Value = SessionManager.Instance.CurrentSyncRun.SyncRunId.ToString();
                    hdMeasurementTypeID.Value = SessionManager.Instance.CurrentProject.ReferenceChartTypeId.ToString();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (SessionManager.Instance.ProfileTypeID == ProfileType.Optimization)
            {
                hdnHasMeasurement.Value = SessionManager.Instance.CurrentSyncRun.HasMeasurements.ToString().ToLower();
            }
        }
        #endregion
    }
}
