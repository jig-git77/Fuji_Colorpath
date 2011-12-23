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
    public partial class AddMeasurementCommon : System.Web.UI.UserControl, IViewMeasurement
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

        public ProfileType Type { get; set; }
        public int pTypeId { get { return Convert.ToInt32(Type); } }
        
        #endregion

        #region Page Event
        
        protected override void OnInit(EventArgs e)
        {
            _presentermeasurement = new PresenterMeasurement(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionManager.CheckLogin();            
            if (!IsPostBack)
            {
                if (Type == ProfileType.DestinationProfile)
                {
                    
                }
                if (Type == ProfileType.Optimization)
                {
                    hdProjectID.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                    hdSyncRunID.Value = SessionManager.Instance.CurrentSyncRun.SyncRunId.ToString();
                    hdMeasurementTypeID.Value = SessionManager.Instance.CurrentProject.ReferenceChartTypeId.ToString();
                }
                this.TypeId = Type;
                hdProfileID.Value = this.ProfileId.ToString();
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (SessionManager.Instance.ProfileTypeID == ProfileType.Optimization)
            {
                hdnHasMeasurement.Value = SessionManager.Instance.CurrentSyncRun.HasMeasurements.ToString().ToLower();
            }
        }
        protected void btnBindGrid_Click(object sender, EventArgs e)
        {
            //BindMeasurement();
        }

        #endregion


        #region IViewMeasurement Mebers
        public int SyncRunId { get; set; }
        public List<Measurement> Measurements { get; set; }
        public int MeasurementId { get; set; }
        public int MeasurementDeleted { get; set; }
        public int ProfileId { get; set; }
        public ProfileType TypeId { get; set; }
        #endregion
    }    
}
