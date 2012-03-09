using System;
using System.Collections.Generic;
using System.Web.Services;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementCommon : System.Web.UI.UserControl
    {
        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public int ProfileId
        {
            get { return Convert.ToInt32(hdProfileID.Value); }
        }

        //public ProfileType Type { get; set; }
        public string Type { get; set; }

        #endregion

        #region Page Event

        protected override void OnInit(EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionManager.CheckLogin();
            //if (!IsPostBack)
            //{
            //    if (Type == ProfileType.Optimization)
            //    {
            //        hdProjectID.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
            //        hdSyncRunID.Value = SessionManager.Instance.CurrentSyncRun.SyncRunId.ToString();
            //        hdMeasurementTypeID.Value = SessionManager.Instance.CurrentProject.ReferenceChartTypeId.ToString();
            //    }
            //    else if (Type == ProfileType.DestinationProfile)
            //    {
            //        hdProfileID.Value = SessionManager.Instance.CurrentDestinationProfile.ProfileId.ToString();
            //        hdMeasurementTypeID.Value = SessionManager.Instance.CurrentDestinationProfile.ReferenceChartTypeId.ToString();
            //        hdSerialNumber.Value = SessionManager.Instance.CurrentDestinationProfile.SerialNumber.ToString();
            //    }
            //}
        }

        #endregion

    }
}
