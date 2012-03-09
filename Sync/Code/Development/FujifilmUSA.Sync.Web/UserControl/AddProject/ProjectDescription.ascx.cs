using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ProjectDescription : System.Web.UI.UserControl
    {
        protected void lnkbtnPrintDescription_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentNavigation = LeftMenuItemLevel.TargetColorSpace;
            ((ProjectSettingsContainer)Parent.Parent).LoadProjectSettingControls();
        }
    }
}