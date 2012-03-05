using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class AddDestinationProfileMeasurement : System.Web.UI.UserControl, IViewAddDestinationProfileMeasurement
    {
        #region "Declaration"
        private PresenterAddDestinationProfileMeasurement _presenteradddestinationprofilemeasurement;
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenteradddestinationprofilemeasurement = new PresenterAddDestinationProfileMeasurement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();
            
        }

        #endregion

        #region Control Events
        protected void lbtnDestinationProfileSave_Click(object sender, EventArgs e)
        {
            ProfileId = ucAddMeasurementCommon.ProfileId;
            ProfilerOptionId =Convert.ToInt32(SessionManager.Instance.CurrentDestinationProfile.ProfileOptionId);
            _presenteradddestinationprofilemeasurement.CreateDestinationProfile();

            Response.Redirect("DestinationProfileList.aspx");
        }
        #endregion

        #region Error Handling Method
        public void ErrorOccured(object sender, Business.Interface.ErrorArgs e)
        {
          ucMeasurementStatusbar.DisplayMessage(e.ErrorMessage, false);
        }
        #endregion

        //*******************************************************************//

        #region IViewAddDeviceLinkProfileMeasurement Members

        public int ProfileId { get; set;}
        public int ProfilerOptionId { get; set; }

        #endregion

    }
}