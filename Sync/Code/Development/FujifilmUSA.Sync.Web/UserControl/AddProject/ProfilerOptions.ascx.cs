using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProfilerOptions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPresets();
            }
        }
        private void BindPresets()
        {
            var dic = new Dictionary<int,string> {{1, "Preserved Black"}};
            cmbSelectedPresets.DataSource = dic;
            cmbSelectedPresets.DataTextField = "Value";
            cmbSelectedPresets.DataValueField = "Key";
            cmbSelectedPresets.DataBind();
        }

        protected void lnkbtnProfilerOptions_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.MeasurementSetup;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();                        
        }
    }
}