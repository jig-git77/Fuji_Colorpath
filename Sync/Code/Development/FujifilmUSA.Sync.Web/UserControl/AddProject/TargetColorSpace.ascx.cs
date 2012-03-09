using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class TargetColorSpace : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProfileSource();
            }
        }
        private void BindProfileSource()
        {
            var dic = new Dictionary<int,string> {{1, "Gracol v02"}, {2, "Swop"}};
            cmbSourceProfile.DataSource = dic;
            cmbSourceProfile.DataTextField = "Value";
            cmbSourceProfile.DataValueField = "Key";
            cmbSourceProfile.DataBind();
        }
        protected void lnkbtnTargetColorSpace_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.ProfilerOption;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();            
        }
    }
}