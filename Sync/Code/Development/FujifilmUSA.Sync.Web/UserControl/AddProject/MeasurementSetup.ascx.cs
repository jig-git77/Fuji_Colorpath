using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementSetup : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMeasurementDevice();
            }
        }

        private void BindMeasurementDevice()
        {
            var dic = new Dictionary<int, string> {{1, "i1-Pro - 873613"}};
            cmbMeasurementDevice.DataSource = dic;
            cmbMeasurementDevice.DataTextField = "Value";
            cmbMeasurementDevice.DataValueField = "Key";
            cmbMeasurementDevice.DataBind();
        }

        protected void lnkbtnMeasurementSetup_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.Save;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();                                    
        }
    }
}