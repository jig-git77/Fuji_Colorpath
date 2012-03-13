using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Web.UI.WebControls;


namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfilerOptions : System.Web.UI.UserControl
    {
        #region "Declaration"
        //private PresenterProjectList _presenterProjectList;
        #endregion


        #region "Page Events"
        protected override void OnInit(EventArgs e)
        {
           // _presenterProjectList = new PresenterProjectList(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPresets();
            }
        }
        #endregion

        #region Method
        private void BindPresets()
        {
            var dic = new Dictionary<int, string> { { 1, "Preserved Black" } };
            cmbSelectedPresets.DataSource = dic;
            cmbSelectedPresets.DataTextField = "Value";
            cmbSelectedPresets.DataValueField = "Key";
            cmbSelectedPresets.DataBind();
        }
        #endregion

        #region Method

        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.MeasurementSetup;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();                        
        }

        #endregion

      

    }
}
